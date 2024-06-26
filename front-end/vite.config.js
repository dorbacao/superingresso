
import { defineConfig, loadEnv } from 'vite';
import { resolve } from 'path';
import glob from 'glob';
import handlebars from 'vite-plugin-handlebars';
import mkcert from 'vite-plugin-mkcert';

const entries = glob.sync('./src/**/*.html').reduce((acc, path) => {
  const name = path.split('/').pop().split('.').shift();
  acc[name] = path;
  return acc;
}, {});

export default defineConfig(({ command, mode }) => {
  const env = loadEnv(mode, process.cwd(), '');

  return {
    root: 'src',
    // vite config
    define: {
      VITE_APP_URL: JSON.stringify(env.VITE_APP_URL),
    },
    plugins: [
      mkcert(),
      handlebars({
        partialDirectory: resolve(__dirname, './src/partials'),
      }),
    ],
    resolve: {
      alias: {
        '@tailwind.config': resolve(__dirname, './tailwind.config.js'),
        '@': resolve(__dirname, './src'),
      }
    },
    optimizeDeps: {
      entries: Object.keys(entries),
    },

    
    server: {
      https: true,
      // ...
      contentSecurityPolicy: {
        directives: {
          scriptSrc: ["'self'", "'unsafe-eval'", 'https://apis.google.com/js/platform.js'],
        }
      }
    },
    build: {
      target: 'esnext',
      //outDir: resolve(__dirname, 'dist'),
      outDir: mode === 'production' ? 'G:\\Other computers\\Servidor\\WebServer\\marcuscarreira.pt\\front-end' : '../dist-development',
      assetsInclude: ['**/*.html'],
      rollupOptions: {
        input: entries,
        output: {
          assetFileNames: (chunkInfo) => {
            let outDir = '';

            // Fonts
            if (/(ttf|woff|woff2|eot)$/.test(chunkInfo.name)) {
              outDir = 'fonts';
            }

            // SVG
            if (/svg$/.test(chunkInfo.name)) {
              outDir = 'svg';
            }

            // Images
            if (/(png|jpg|jpeg|gif|webp)$/.test(chunkInfo.name)) {
              outDir = 'images';
            }

            // Media
            if (/(mp3|mp4|webm|ogg|wav|flac|aac)$/.test(chunkInfo.name)) {
              outDir += 'media';
            }

            // JSON
            if (/json$/.test(chunkInfo.name)) {
              outDir = 'json';
            }

            // JS
            if (/js$/.test(chunkInfo.name)) {
              outDir = 'js';
            }

            // CSS
            if (/css$/.test(chunkInfo.name)) {
              outDir = 'css';
            }

            return `${outDir}/[name][extname]`;
          },
          chunkFileNames: 'js/[name]-[hash].js',
          entryFileNames: 'js/[name]-[hash].js',

        }
      },
    },
  };
});
