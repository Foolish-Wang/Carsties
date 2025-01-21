// import EmptyFilter from "@/app/components/EmptyFilter";
//
// export default function Page({searchParams}: {searchParams: {callbackUrl: string}}) {
//     return (
//         <EmptyFilter
//             title='You need to be logged in to do that'
//             subtitle='Please click below to sign in'
//             showLogin
//             callbackUrl={searchParams.callbackUrl}
//         />
//     )
// }

import EmptyFilter from "@/app/components/EmptyFilter";

export default function Page({searchParams}: {searchParams: {callbackUrl: string}}) {
    return (
        <EmptyFilter
            title='You need to be logged in to do that'
            subtitle='Please click below to sign in'
            showLogin
            callbackUrl={searchParams.callbackUrl}
        />
    )
}

// import EmptyFilter from "@/app/components/EmptyFilter";
//
// type SearchParams = Promise<{ callbackUrl: string }>;
//
// export default async function Page({
//                                        searchParams
//                                    }: {
//     searchParams: SearchParams;
// }) {
//     const resolvedSearchParams = await searchParams;
//
//     return (
//         <EmptyFilter
//             title='You need to be logged in to do that'
//             subtitle='Please click below to sign in'
//             showLogin
//             callbackUrl={resolvedSearchParams.callbackUrl}
//         />
//     );
// }










