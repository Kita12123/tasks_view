---
name: openapi-validation
description: Audit and apply strict OpenAPI 3.0 validation keywords to openapi.yaml based on data types to ensure schema integrity and security.
---

# Validation

## When to Use
- When defining or updating schemas in `openapi.yml`.
- When performing a security audit to prevent "Insecure Input Validation".
- When defining business rules and constraints for API inputs.

## Core Principles
- Validations supported by OpenAPI should be defined natively using OpenAPI attributes.

## Workflow
1. **File Analysis:** Read the `openapi.yml` file and identify components or paths requiring validation.
2. **Type Identification:** Detect the `type` for each property (string, integer, number, array, or object).
3. **Requirement Gathering:** Determine business limits (e.g., "Age cannot be negative").
4. **Keyword Application:** Apply the specific validation keywords defined in the **Validation Attributes by Type** section below.
5. **Approval & Write:** Present a diff of the `openapi.yml` changes and apply them upon user confirmation.

## Validation Attributes by Type

### Number & Integer
Apply these keywords to `type: number` or `type: integer`:
- **Range:** Use `minimum` and `maximum` to define allowed values.
- **Exclusion:** Use `exclusiveMinimum: true` or `exclusiveMaximum: true` to exclude the boundary value from the range.
- **Multiples:** Use `multipleOf` to ensure a number is a multiple of another (e.g., `multipleOf: 10`).
- **Format:** Specify `int32`, `int64`, `float`, or `double` as a hint for tools.

### String
Apply these keywords to `type: string`:
- **Length:** Restrict length using `minLength` and `maxLength`.
- **Pattern:** Use `pattern` with a JavaScript-compliant regular expression (wrapped in `^` and `$`) for exact matches.
- **Standard Formats:** Use built-in formats like `date`, `date-time`, `password`, `email`, or `uuid`.

### Array
Apply these keywords to `type: array`:
- **Items:** Ensure the mandatory `items` keyword is present to define the element schema.
- **Size:** Set `minItems` and `maxItems` to restrict array length.
- **Uniqueness:** Use `uniqueItems: true` to prevent duplicate elements in the collection.

### Object
Apply these keywords to `type: object`:
- **Mandatory Fields:** List required property names in the object-level `required` array.
- **Property Limits:** Use `minProperties` and `maxProperties` to restrict the number of allowed properties, especially for free-form objects.
- **Strictness:** Control whether extra properties are allowed using `additionalProperties`.

## Execution Constraints
- **Nullable Support:** Since OpenAPI 3.0 lacks a null type, use `nullable: true` when a value can be null.
- **Exact Matches:** Always enclose `pattern` values in `^...$` to force an exact match and avoid partial-match security risks.
- **No Manual-only Edits:** The agent must preview the `openapi.yml` changes in a structured format before saving.
