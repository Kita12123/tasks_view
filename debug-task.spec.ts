import { test, expect } from '@playwright/test';

test.setTimeout(120000);

test('Add new task with debugging', async ({ page }) => {
  // Set up console and network logging
  page.on('console', msg => console.log(`[BROWSER CONSOLE] ${msg.text()}`));
  page.on('response', response => {
    if (response.url().includes('api') || response.status() >= 400) {
      console.log(`[API RESPONSE] ${response.url()}: ${response.status()}`);
    }
  });
  
  // Navigate to the app
  await page.goto('http://localhost:8080', { waitUntil: 'domcontentloaded', timeout: 60000 });
  
  // Wait for the page to fully load
  await page.waitForTimeout(2000);
  
  // Click the "＋ タスクを追加" button to open the modal
  const addButton = page.locator('button:has-text("＋ タスクを追加")');
  await expect(addButton).toBeVisible({ timeout: 10000 });
  console.log('Clicking add button...');
  await addButton.click();
  
  // Wait for the modal to appear
  await page.waitForTimeout(1000);
  
  // Fill in the title field
  console.log('Filling title field...');
  const titleInput = page.locator('#title');
  await expect(titleInput).toBeVisible({ timeout: 5000 });
  await titleInput.fill('UI Test Task');
  
  // Fill in the content field
  console.log('Filling content field...');
  const contentInput = page.locator('#content');
  await expect(contentInput).toBeVisible({ timeout: 5000 });
  await contentInput.fill('Created via Playwright UI test');
  
  // Select tomorrow's date
  console.log('Setting date field...');
  const dateInput = page.locator('#duedate');
  await expect(dateInput).toBeVisible({ timeout: 5000 });
  
  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  tomorrow.setHours(12, 0);
  
  const year = tomorrow.getFullYear();
  const month = String(tomorrow.getMonth() + 1).padStart(2, '0');
  const day = String(tomorrow.getDate()).padStart(2, '0');
  const hours = String(tomorrow.getHours()).padStart(2, '0');
  const minutes = String(tomorrow.getMinutes()).padStart(2, '0');
  
  const dateTimeLocalValue = `${year}-${month}-${day}T${hours}:${minutes}`;
  
  console.log(`Setting date to: ${dateTimeLocalValue}`);
  await dateInput.fill(dateTimeLocalValue);
  
  // Trigger the change event
  await dateInput.evaluate((el: HTMLInputElement) => {
    el.dispatchEvent(new Event('change', { bubbles: true }));
  });
  
  // Take a screenshot before saving
  await page.screenshot({ path: 'before-save.png' });
  
  // Click the save button
  console.log('Clicking save button...');
  const saveButton = page.locator('button:has-text("保存")');
  await expect(saveButton).toBeVisible({ timeout: 5000 });
  await saveButton.click();
  
  // Wait longer for the API call to complete and page to update
  console.log('Waiting for task to be added...');
  await page.waitForTimeout(5000);
  
  // Take a screenshot after saving
  await page.screenshot({ path: 'after-save.png' });
  
  // Check if there are any error messages on the page
  const errorMessages = page.locator('[class*="destructive"]');
  const errorCount = await errorMessages.count();
  console.log(`Found ${errorCount} error messages`);
  for (let i = 0; i < errorCount; i++) {
    const text = await errorMessages.nth(i).textContent();
    console.log(`Error: ${text}`);
  }
  
  // Check the page content
  const pageText = await page.textContent('body');
  console.log(`Page contains 'UI Test Task': ${pageText?.includes('UI Test Task')}`);
  console.log(`Page contains 'Created via Playwright': ${pageText?.includes('Created via Playwright')}`);
  
  // Get the table content to debug
  const rows = page.locator('tbody tr');
  const rowCount = await rows.count();
  console.log(`Found ${rowCount} tasks in the table`);
  
  for (let i = 0; i < Math.min(rowCount, 5); i++) {
    const cells = rows.nth(i).locator('td');
    const title = await cells.nth(0).textContent();
    const content = await cells.nth(1).textContent();
    console.log(`Task ${i}: title="${title}", content="${content}"`);
  }
});
