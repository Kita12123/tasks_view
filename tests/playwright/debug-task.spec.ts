import { expect, test } from '@playwright/test';

test.setTimeout(60000);

test('Add new task with debugging', async ({ page }) => {
  // Set up console and network logging
  page.on('console', msg => console.log(`[BROWSER CONSOLE] ${msg.text()}`));
  page.on('response', response => {
    if (response.url().includes('api') || response.status() >= 400) {
      console.log(`[API RESPONSE] ${response.url()}: ${response.status()}`);
    }
  });

  // Helper: robust navigation with retries to avoid transient network errors
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

  // Click the factory Create button which navigates to /tasks/new
  const addButton = page.getByRole('button', { name: /タスクを追加|Add task/i });
  await expect(addButton).toBeVisible({ timeout: 10000 });

  // Mock backend create API
  await page.route('**/api/tasks**', async route => {
    await route.fulfill({ status: 201, contentType: 'application/json', body: JSON.stringify({}) });
  });

  await Promise.all([
    page.waitForURL('**/tasks/new', { timeout: 10000 }),
    addButton.click(),
  ]);

  // Wait a little for create page to render
  await page.waitForTimeout(500);

  // Fill in the title field
  console.log('Filling title field...');
  // Use first visible input/textarea fields inside modal
  const fields = page.locator('input, textarea');
  await expect(fields.first()).toBeVisible({ timeout: 5000 });
  await fields.nth(0).fill('UI Test Task');

  // Fill in the content field
  console.log('Filling content field...');
  await fields.nth(1).fill('Created via Playwright UI test');

  // Select tomorrow's date
  console.log('Setting date field...');
  const dateInput = page.locator('input[type="date"], input[type="datetime-local"]');

  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  tomorrow.setHours(12, 0);

  const year = tomorrow.getFullYear();
  const month = String(tomorrow.getMonth() + 1).padStart(2, '0');
  const day = String(tomorrow.getDate()).padStart(2, '0');
  const hours = String(tomorrow.getHours()).padStart(2, '0');
  const minutes = String(tomorrow.getMinutes()).padStart(2, '0');

  const dateTimeLocalValue = `${year}-${month}-${day}T${hours}:${minutes}`;

  if (await dateInput.count() > 0) {
    await dateInput.first().fill(dateTimeLocalValue);
    await dateInput.first().evaluate((el: HTMLInputElement) => el.dispatchEvent(new Event('change', { bubbles: true })));
  } else if (await fields.count() > 2) {
    await fields.nth(2).fill(dateTimeLocalValue);
    await fields.nth(2).evaluate((el: HTMLInputElement) => el.dispatchEvent(new Event('change', { bubbles: true })));
  }

  console.log(`Setting date to: ${dateTimeLocalValue}`);

  // Take a screenshot before saving
  await page.screenshot({ path: 'tests/before-save.png' });

  // Click the save button
  console.log('Clicking save button...');
  const saveButton = page.locator('button:has-text("保存")');
  await expect(saveButton).toBeVisible({ timeout: 5000 });
  await saveButton.click();

  // Wait longer for the API call to complete and page to update
  console.log('Waiting for task to be added...');
  await page.waitForTimeout(5000);

  // Take a screenshot after saving
  await page.screenshot({ path: 'tests/after-save.png' });

  // Verify modal closed (backend-independent)
  const modalHeader = page.getByText(/新規タスク作成|タスク編集/);
  if (await modalHeader.count() > 0) {
    await expect(modalHeader.first()).toBeHidden({ timeout: 10000 });
  } else {
    const fieldsAfter = page.locator('input, textarea');
    await expect(fieldsAfter.first()).toBeHidden({ timeout: 10000 });
  }

  console.log('Modal closed after save (backend-independent check)');
});
