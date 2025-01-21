// /** @type {import('next').NextConfig} */
// const nextConfig = {
//     logging:{
//         fetches:{
//             fullUrl: true
//         }
//     },
//     experimental: {
//         // serverActions: true
//     },
//     images: {
//         domains: [
//             'cdn.pixabay.com',
//             'media.istockphoto.com'
//         ]
//     }
// }
//
// module.exports = nextConfig

/** @type {import('next').NextConfig} */
// const nextConfig = {
//     logging: {
//         fetches: {
//             fullUrl: true
//         }
//     },
//     experimental: {
//         // serverActions: true
//     },
//     images: {
//         remotePatterns: [
//             {
//                 protocol: 'https',
//                 hostname: 'cdn.pixabay.com',
//                 pathname: '/**' // 匹配该域名下的所有路径
//             },
//             {
//                 protocol: 'https',
//                 hostname: 'media.istockphoto.com',
//                 pathname: '/**' // 匹配该域名下的所有路径
//             }
//         ]
//     }
// };
//
// module.exports = nextConfig;

/** @type {import('next').NextConfig} */
const nextConfig = {
    logging: {
        fetches: {
            fullUrl: true
        }
    },
    experimental: {
        // serverActions: true
    },
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: 'cdn.pixabay.com',
                pathname: '/**' // 匹配该域名下的所有路径
            },
            {
                protocol: 'https',
                hostname: 'media.istockphoto.com',
                pathname: '/**' // 匹配该域名下的所有路径
            }
        ]
    },
    typescript: {
        ignoreBuildErrors: true, // 忽略 TypeScript 构建错误
    },
    output:'standalone'
};

module.exports = nextConfig;