import { test, expect } from '@playwright/test';

test.setTimeout(120000);

test('Add new task with UI test data', async ({ page }) => {
  // Navigate to the app
  await page.goto('http://localhost:8080', { waitUntil: 'domcontentloaded', timeout: 60000 });
  
  // Wait for the page to fully load
  await page.waitForTimeout(2000);
  
  // Click the "＋ タスクを追加" button to open the modal
  const addButton = page.locator('button:has-text("＋ タスクを追加")');
  await expect(addButton).toBeVisible({ timeout: 10000 });
  console.log('✓ Add button found');
  await addButton.click();
  
  // Wait for the modal to appear
  await page.waitForTimeout(1000);
  
  // Fill in the title field using the id attribute
  console.log('Filling form fields...');
  const titleInput = page.locator('#title');
  await expect(titleInput).toBeVisible({ timeout: 5000 });
  await titleInput.fill('UI Test Task');
  
  // Fill in the content field using the id attribute
  const contentInput = page.locator('#content');
  await expect(contentInput).toBeVisible({ timeout: 5000 });
  await contentInput.fill('Created via Playwright UI test');
  
  // Select tomorrow's date
  const dateInput = page.locator('#duedate');
  await expect(dateInput).toBeVisible({ timeout: 5000 });
  
  // Calculate tomorrow's date in datetime-local format (YYYY-MM-DDTHH:mm)
  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  tomorrow.setHours(12, 0); // Set to noon
  
  const year = tomorrow.getFullYear();
  const month = String(tomorrow.getMonth() + 1).padStart(2, '0');
  const day = String(tomorrow.getDate()).padStart(2, '0');
  const hours = String(tomorrow.getHours()).padStart(2, '0');
  const minutes = String(tomorrow.getMinutes()).padStart(2, '0');
  
  const dateTimeLocalValue = `${year}-${month}-${day}T${hours}:${minutes}`;
  
  console.log(`✓ Form filled with title, content, and date: ${dateTimeLocalValue}`);
  await dateInput.fill(dateTimeLocalValue);
  
  // Trigger the change event
  await dateInput.evaluate((el: HTMLInputElement) => {
    el.dispatchEvent(new Event('change', { bubbles: true }));
  });
  
  // Click the save button (保存)
  console.log('Clicking save button...');
  const saveButton = page.locator('button:has-text("保存")');
  await expect(saveButton).toBeVisible({ timeout: 5000 });
  await saveButton.click();
  
  // Wait for the API call to complete and the task to appear in the table
  console.log('Waiting for task to appear in the list...');
  await page.waitForTimeout(3000);
  
  // Verify the new task appears in the table
  const taskTitle = page.locator('text=UI Test Task').first();
  await expect(taskTitle).toBeVisible({ timeout: 10000 });
  console.log('✓ Task title found in the page');
  
  // Verify the task has correct content in the same row
  const taskContent = page.locator('text=Created via Playwright UI test').first();
  await expect(taskContent).toBeVisible({ timeout: 10000 });
  console.log('✓ Task content found in the page');
  
  // Extra verification: Check that the task appears in the table with both title and content
  const rows = page.locator('tbody tr');
  const rowCount = await rows.count();
  
  let found = false;
  for (let i = 0; i < rowCount; i++) {
    const cells = rows.nth(i).locator('td');
    const title = await cells.nth(0).textContent();
    const content = await cells.nth(1).textContent();
    
    if (title?.includes('UI Test Task') && content?.includes('Created via Playwright UI test')) {
      found = true;
      console.log(`✓ Task verified in table row ${i}`);
      break;
    }
  }
  
  if (!found) {
    throw new Error('Task not found in the table with correct title and content');
  }
  
  console.log('✓ SUCCESS: Task successfully added with correct title and content!');
});
