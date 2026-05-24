import { test, expect } from '@playwright/test';

test.setTimeout(120000);

test('Click add button and inspect modal', async ({ page }) => {
  // Navigate to the app
  await page.goto('http://localhost:8080', { waitUntil: 'domcontentloaded', timeout: 60000 });
  
  // Wait a bit for rendering
  await page.waitForTimeout(3000);
  
  // Click the first button (Button 0) which should be the add button
  const addButton = page.locator('button').first();
  console.log('Clicking add button...');
  await addButton.click();
  
  // Wait for modal to appear
  await page.waitForTimeout(2000);
  
  // Take a screenshot of the modal
  await page.screenshot({ path: 'tests/modal-opened.png' });
  
  // Find all inputs and textareas in the page
  const inputs = page.locator('input');
  const inputCount = await inputs.count();
  console.log(`Found ${inputCount} input elements`);
  
  for (let i = 0; i < inputCount; i++) {
    const type = await inputs.nth(i).getAttribute('type');
    const placeholder = await inputs.nth(i).getAttribute('placeholder');
    const name = await inputs.nth(i).getAttribute('name');
    console.log(`Input ${i}: type=${type}, placeholder="${placeholder}", name="${name}"`);
  }
  
  const textareas = page.locator('textarea');
  const textareaCount = await textareas.count();
  console.log(`Found ${textareaCount} textarea elements`);
  
  for (let i = 0; i < textareaCount; i++) {
    const placeholder = await textareas.nth(i).getAttribute('placeholder');
    const name = await textareas.nth(i).getAttribute('name');
    const visible = await textareas.nth(i).isVisible();
    console.log(`Textarea ${i}: placeholder="${placeholder}", name="${name}", visible=${visible}`);
  }
  
  // Find all buttons in the modal
  const allButtons = page.locator('button');
  const allButtonsCount = await allButtons.count();
  console.log(`Found ${allButtonsCount} buttons total`);
  
  for (let i = 0; i < allButtonsCount; i++) {
    const text = await allButtons.nth(i).textContent();
    const visible = await allButtons.nth(i).isVisible();
    console.log(`Button ${i}: text="${text?.trim()}", visible=${visible}`);
  }
  
  // Get the HTML of the main content area to understand the structure
  const mainContent = await page.locator('main').innerHTML();
  console.log('Main content HTML length:', mainContent.length);
});
