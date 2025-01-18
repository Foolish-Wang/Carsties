'use client'

import { signIn as signInFunc } from 'next-auth/react'
import { Button } from "flowbite-react"

export default function LoginButton(){
    return(
        <Button outline 
                onClick={()=> signInFunc('id-server',{callbackUrl:'/'},{prompt:'login'})}
        >
            Login
        </Button>
    )
}

// 'use client'
//
// import { signIn as signInFunc } from 'next-auth/react'
//
// export default function LoginButton() {
//     return (
//         <button
//             onClick={() => signInFunc('id-server', { callbackUrl: '/' })}
//             className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
//         >
//             Login
//         </button>
//     )
// }