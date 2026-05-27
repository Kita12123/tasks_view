import { expect, test } from '@playwright/test';

test('tasks page shows list and create button', async ({ page }) => {
  // Mock server API responses to avoid backend dependency
  await page.route('**/tasks**', async route => {
    const req = route.request();
    const accept = (req.headers()['accept'] || '').toString();
    // Allow navigation requests for HTML to continue to the server
    if (accept.includes('text/html')) {
      await route.continue();
      return;
    }
    await route.fulfill({
      status: 200,
      contentType: 'application/json',
      body: JSON.stringify({ Items: [] })
    });
  });

  // Robust navigation helper
  async function retryGoto(path: string, attempts = 3) {
    for (let i = 0; i < attempts; i++) {
      try {
        await page.goto(path, { waitUntil: 'load', timeout: 10000 });
        return;
      } catch (e) {
        if (i === attempts - 1) throw e;
        console.warn(`goto failed, retrying... (${i + 1})`);
        await page.waitForTimeout(1000);
      }
    }
  }

  await retryGoto('/tasks');

  // Heading exists
  const heading = page.getByRole('heading', { name: /タスク|Tasks/i });
  await expect(heading.first()).toBeVisible();

  // Create button visible
  const createBtn = page.getByRole('button', { name: /タスクを追加|Add task|\+\s*タスクを追加/i });
  await expect(createBtn).toBeVisible();

  // Task list area exists OR empty state message
  const listLocator = page.locator('.task-list-wrapper');
  const altLocator = page.getByText(/タスクはありません|No tasks|タスクがありません/i);
  if (await listLocator.count() > 0) {
    await expect(listLocator.first()).toBeVisible();
  }
  else {
    await expect(altLocator).toBeVisible();
  }
});
