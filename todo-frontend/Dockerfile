FROM node:20-alpine AS builder

WORKDIR /app

COPY todo-frontend/package*.json ./

RUN npm install

COPY ./todo-frontend .

RUN npm run ng build --production

FROM nginx:alpine

COPY --from=builder /app/dist/todo-frontend/browser/ /usr/share/nginx/html

COPY todo-frontend/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]