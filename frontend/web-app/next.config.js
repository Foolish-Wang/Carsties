/** @type {import('next').NextConfig} */
const nextConfig = {
    logging:{
        fetches:{
            fullUrl: true
        }
    },
    experimental: {
        // serverActions: true
    },
    images: {
        domains: [
            'cdn.pixabay.com'
        ]
    }
}

module.exports = nextConfig