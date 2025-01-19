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
            'cdn.pixabay.com',
            'media.istockphoto.com'
        ]
    }
}

module.exports = nextConfig