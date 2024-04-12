import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
<<<<<<< HEAD
    plugins: [react()],
    server: {
        proxy: {
            // localhost:5173/api -> localhost:3000/,
            // t.ex. localhost:5173/api/products -> localhost:3000/products
            '/api': {
                // url som vi vill skicka till, från vår react app
                target: 'http://localhost:3008',
                changeOrigin: true,
                // tar bort /api/ från vår path
                rewrite: (path) => path.replace(/^\/api/, ''),
            }
        }
    }
})
=======
  plugins: [react()],
  server: {
    proxy: {
      // localhost:5173/api -> localhost:3000/,
      // t.ex. localhost:5173/api/products -> localhost:3000/products
      "/api": {
        // url som vi vill skicka till, från vår react app
        target: "http://localhost:3008",
        changeOrigin: true,
        // tar bort /api/ från vår path
        rewrite: (path) => path.replace(/^\/api/, ""),
      },
    },
  },
});
>>>>>>> origin/jgdevelop
