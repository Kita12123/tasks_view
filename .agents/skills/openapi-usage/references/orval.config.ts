import { defineConfig } from 'orval';

export default defineConfig({
    petstore: {
        input: './openapi.yml',
        output: {
            client: 'zod', // クライアントとしてZodスキーマを出力
            target: './src/api/zod',
        },
    },
});