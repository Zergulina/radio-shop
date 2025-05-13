# Build stage
FROM node:19 AS builder
WORKDIR /app
COPY . .
RUN npm install && npm run build

# Serve stage
FROM nginx:1.19.6-alpine
COPY --from=builder /app/dist /usr/share/nginx/html
RUN mkdir /etc/nginx/templates
COPY ./nginx.conf.template etc/nginx/templates