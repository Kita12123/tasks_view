---
name: glossary
description: This is a glossary for AI agents that I want AI agents to follow when working with me. You can use this document as a reference when working with me or when adding or modifying documents and glossary.
tags:
    - skill
---
# Terms Types
There are three types of terms in this glossary:
- Standard Terms: These are the standard terms that should be used in all documents and coding.
- Forbidden Terms: These are the terms that should not be used in any documents or coding.
- Domain Terms: These are the terms that are specific to a particular domain or context.

Headers:
- Term: The term that is being defined.
- 日本語名: The Japanese name for the term.
- Alias: The alias for the term that can be used in documents and coding. The recommended length is 4 characters or less.
- Avoid: The terms that should be avoided when using the term.
- Definition: The definition of the term that explains its meaning and how it should be used.
- Use instead: The term that should be used instead of the forbidden term.
- 代替語: The Japanese term that should be used instead of the forbidden term.
- Reason: The reason why the term is forbidden and should not be used.

# Standard Terms
| Term | 日本語名 | Alias | Avoid | Definition |
| --- | --- | --- | --- | --- |
| ai-agent | AIエージェント | AI | ai-assistant |  An AI agent is you. |
| glossary | 用語集 | Glossary | ubiquitous, wordy |  A glossary is a list of terms and their definitions. |
| document | ドキュメント | Doc | note | A document is a markdown file that contains information about a specific topic. |
| source-code | ソースコード | Src | programming | Source code is the human-readable instructions that a programmer writes to create applications. |
| feature | 機能 | Feature | specification, functionality | A feature is a specific functionality or capability of a system or application. |
| frontmatter | フロントマター | Frontmatter | property | A frontmatter is a key-value pair that is used to describe a document. It is defined in the YAML frontmatter of a markdown file. |
| coding | コーディング | Coding | programming | Coding is the process of writing code to create applications. |
| workflow | ワークフロー | Workflow | process | A workflow is a series of steps that are followed to complete a task or achieve a goal. |
| wireframe | ワイヤーフレーム | Wireframe | mockup | A wireframe is a visual representation of a user interface that shows the layout and structure of the interface without any design elements. |
| application | アプリケーション | App | software | An application is a software program that is designed to perform a specific function or set of functions. |

# Forbidden Terms
| Term | 日本語名 | Use instead | 代替語 | Reason |
| --- | --- | --- | --- | --- |
| data | データ | ～Dto | ～データ | "data" is too generic and can cause confusion. So it is recommended to use more specific terms like "UserDto" or "ProductDto". |

## Domain Terms
| Term | 日本語名 | Alias | Avoid | Definition |
| --- | --- | --- | --- | --- |
| get | 取得 | Get | fetch, retrieve, find | To get something means to obtain it from a document or coding. |
| add | 追加 | Add | create, register | To add something means to include it in a document or coding. |
| update | 更新 | Upd | modify, edit | To update something means to make changes to it in order to improve it or keep it current. |
| delete | 削除 | Del | remove | To delete something means to remove it from a document or coding. |
| bulk | 一括 | Bulk | batch | Bulk refers to performing an action on multiple items at once, rather than individually. Example: BulkGet, BulkDel |
| request | リクエスト | Req | parameter, input | A request is an action for processing information or performing a task. |
| response | レスポンス | Dto | output | A response is the result of a request. Define the response data as a DTO (Data Transfer Object). |
