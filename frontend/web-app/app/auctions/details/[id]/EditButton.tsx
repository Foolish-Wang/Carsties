'use client'

import { Button } from 'flowbite-react'
import Link from 'next/link'
import React from 'react'

type Props = {
    id: string
}

export default function EditButton({id}: Props) {
    return (
        <Button outline className="border border-gray-500 text-gray-700 hover:bg-gray-200">
            <Link href={`/auctions/update/${id}`}>Update Auction</Link>
        </Button>
    )
}