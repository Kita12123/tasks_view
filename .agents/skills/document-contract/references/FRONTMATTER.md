---
name: frontmatter
description: This document explains the frontmatter properties that should be used in markdown documents. Frontmatter is a key-value pair that is used to describe a document. It is defined in the YAML frontmatter of a markdown file. You can use this document as a reference when working with me or when adding or modifying documents.
tags:
    - skill
---
# Frontmatter Reference
Properties use YAML frontmatter at the start of a note:

```yaml
---
name: Title of this document
description: A brief description of the document's content and purpose.
tags:
  - skill
  - feature
thumb: https://example.com/image.jpg
env:
  - API_KEY
---
```

## Property Types
| Property    | Type      | Required | Definition |
|-------------|-----------|----------|------------|
| name        | Text      | Yes      | The title of the document. |
| description | Text      | Yes      | A brief description of the document's content and purpose. |
| tags        | YAML List | No       | A list of tags for the document. |
| cover       | Text      | No       | The URL or file path for the document's cover image. |
| env         | YAML List | No       | A list of required environment variables for the document. |

## Tags
Use `tags` to categorize documents.
```yaml
tags:
  - skill
```

Pick tags from the following list:
| Tag     | Definition |
|---------|------------|
| skill   | A tag for instructions of ai-agents in `skills` directory. |
| feature | A tag for defining feature or specification in `docs` directory. |
| page    | A tag for defining a how-to view, page-layout in `docs` directory. |

## Cover Image
Use `cover` to specify an image URL or a local file path for the document's cover.
```yaml
thumb: https://example.com/image.jpg
```

## Environment Variables
Use `env` to list required environment variables for the document's content or functionality.
```yaml
env:
  - API_KEY
```