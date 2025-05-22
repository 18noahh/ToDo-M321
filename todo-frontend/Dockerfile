# Stage 1: Build the Angular application
FROM node:20 AS builder

WORKDIR /app

# Copy package.json and package-lock.json (only these to leverage caching)
COPY todo-frontend/package*.json ./

# If package-lock.json exists, use `npm ci`, otherwise fallback to `npm install`
# (This is a trick using shell logic)
RUN [ -f package-lock.json ] && npm ci || npm install

# Copy the rest of the source code
COPY todo-frontend/ .


RUN npm run build --configuration todo-frontend

# Stage 2: Serve the app with Nginx
FROM nginx:alpine

# Copy built files from builder
COPY --from=builder /app/dist/todo-frontend /usr/share/nginx/html

# Optional: Add a custom Nginx config
# COPY todo-frontend/nginx.conf /etc/nginx/nginx.conf

# Expose default port
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
