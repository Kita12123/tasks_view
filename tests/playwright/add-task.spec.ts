import { expect, test } from '@playwright/test';

test.setTimeout(60000);

test('Add new task via create page (backend-independent)', async ({ page }) => {
  // Helper: robust navigation with retries
  async function retryGoto(url: string, attempts = 3) {
    for (let i = 0; i < attempts; i++) {
      try {
        await page.goto(url, { waitUntil: 'domcontentloaded', timeout: 60000 });
        return;
      } catch (e) {
        if (i === attempts - 1) throw e;
        console.warn(`goto failed, retrying... (${i + 1})`);
        await page.waitForTimeout(1000);
      }
    }
  }

  await retryGoto('/');

  // Click the create button which navigates to /tasks/new
  const addButton = page.getByRole('button', { name: /タスクを追加|Add task/i });
  await expect(addButton).toBeVisible({ timeout: 10000 });

  // Intercept backend create so test doesn't depend on API
  await page.route('**/api/tasks**', async route => {
    await route.fulfill({ status: 201, contentType: 'application/json', body: JSON.stringify({}) });
  });

  await Promise.all([
    page.waitForURL('**/tasks/new', { timeout: 10000 }),
    addButton.click(),
  ]);

  // Fill form on /tasks/new
  const title = page.locator('#title');
  await expect(title).toBeVisible({ timeout: 5000 });
  await title.fill('UI Test Task');

  const content = page.locator('#content');
  await expect(content).toBeVisible({ timeout: 5000 });
  await content.fill('Created via Playwright UI test');

  const dateInput = page.locator('#duedate, input[type="date"], input[type="datetime-local"]');
  if (await dateInput.count() > 0) {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    tomorrow.setHours(12, 0, 0, 0);
    const yyyy = tomorrow.getFullYear();
    const mm = String(tomorrow.getMonth() + 1).padStart(2, '0');
    const dd = String(tomorrow.getDate()).padStart(2, '0');
    const hh = String(tomorrow.getHours()).padStart(2, '0');
    const min = String(tomorrow.getMinutes()).padStart(2, '0');
    const value = `${yyyy}-${mm}-${dd}T${hh}:${min}`;
    await dateInput.first().fill(value).catch(() => { });
  }

  // Click save and wait for navigation back to /tasks
  const save = page.getByRole('button', { name: /保存|Save/i });
  await expect(save).toBeVisible({ timeout: 5000 });
  await Promise.all([
    page.waitForURL('**/tasks', { timeout: 60000 }),
    save.click(),
  ]);

  // Confirm we are back on tasks page
  await expect(page).toHaveURL(/\/tasks/);
});
