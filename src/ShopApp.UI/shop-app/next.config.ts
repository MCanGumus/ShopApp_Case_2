/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    turbo: false,   // dev ve build'te turbopack'i tamamen kapatır
  },
};

module.exports = nextConfig;