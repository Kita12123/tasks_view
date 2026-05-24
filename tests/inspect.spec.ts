import { test, expect } from '@playwright/test';

test.setTimeout(120000);

test('Inspect page structure', async ({ page }) => {
  // Navigate to the app
  await page.goto('http://localhost:8080', { waitUntil: 'domcontentloaded', timeout: 60000 });
  
  // Wait a bit for rendering
  await page.waitForTimeout(3000);
  
  // Take a screenshot to see what's on the page
  await page.screenshot({ path: 'tests/page-before-click.png' });
  
  // Find the add button by various selectors
  console.log('Looking for add button...');
  
  // Try finding by text content
  const buttons = page.locator('button');
  const count = await buttons.count();
  console.log(`Found ${count} buttons`);
  
  for (let i = 0; i < Math.min(count, 10); i++) {
    const text = await buttons.nth(i).textContent();
    console.log(`Button ${i}: "${text}"`);
  }
  
  // Look for the specific add button
  const addButtonVariants = [
    page.locator('button:has-text("＋ タスクを追加")'),
    page.locator('button:has-text("+")'),
    page.locator('button:contains("タスクを追加")')
  ];
  
  for (let i = 0; i < addButtonVariants.length; i++) {
    const visible = await addButtonVariants[i].isVisible({ timeout: 1000 }).catch(() => false);
    console.log(`Variant ${i} visible: ${visible}`);
  }
  
  // Try to click the first button that contains the plus sign
  const allText = await page.locator('*').evaluateAll((elements) => {
    return elements
      .filter(el => el.textContent?.includes('＋'))
      .slice(0, 5)
      .map(el => ({
        tag: el.tagName,
        text: el.textContent?.substring(0, 100),
        class: el.className
      }));
  });
  
  console.log('Elements with plus sign:', allText);
});
