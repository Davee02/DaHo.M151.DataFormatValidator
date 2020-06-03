FROM node:14.3.0-alpine AS build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.19.0-alpine
COPY --from=build /usr/src/app/dist/dataformvalidator /usr/share/nginx/html