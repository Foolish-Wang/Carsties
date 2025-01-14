// 'use client'
// //
// // import { Pagination } from 'flowbite-react'
// // import React, { useState } from 'react'
// //
// // type Props = {
// //     currentPage: number
// //     pageCount: number
// //     pageChanged: (page: number) => void;
// // }
// //
// // export default function AppPagination({ currentPage, pageCount, pageChanged }: Props) {
// //     return (
// //         <Pagination
// //             currentPage={currentPage}
// //             onPageChange={e => pageChanged(e)}
// //             totalPages={pageCount}
// //             layout='pagination'
// //             showIcons={true}
// //             className='text-blue-500 mb-5'
// //         />
// //     )
// // }

'use client'

import React from 'react'

type Props = {
    currentPage: number
    pageCount: number
    pageChanged: (page: number) => void
}

export default function AppPagination({ currentPage, pageCount, pageChanged }: Props) {
    const getPages = () => {
        const pages: (number | string)[] = []

        if (currentPage > 3) {
            pages.push(1)
            if (currentPage > 4) {
                pages.push('...')
            }
        }

        for (let i = Math.max(1, currentPage - 1); i <= Math.min(pageCount, currentPage + 1); i++) {
            pages.push(i)
        }

        if (currentPage < pageCount - 2) {
            if (currentPage < pageCount - 3) {
                pages.push('...')
            }
            pages.push(pageCount)
        }

        return pages
    }

    const buttonBaseStyles = "min-w-[40px] h-10 flex items-center justify-center text-sm font-medium transition-colors duration-200"
    const pageButtonStyles = "border border-gray-300 hover:bg-gray-50 text-gray-700"
    const activePageStyles = "bg-blue-600 text-white border-blue-600 hover:bg-blue-700"
    const navigationButtonStyles = "px-4 border border-gray-300 hover:bg-gray-50 text-gray-700"
    const disabledStyles = "opacity-50 cursor-not-allowed hover:bg-white"

    return (
        <nav className="flex justify-center items-center gap-3 my-8">
            <button
                className={`${buttonBaseStyles} ${navigationButtonStyles} rounded-l-lg ${currentPage === 1 ? disabledStyles : ''}`}
                onClick={() => pageChanged(currentPage - 1)}
                disabled={currentPage === 1}
            >
                Previous
            </button>

            <div className="flex gap-2">
                {getPages().map((page, i) => (
                    <button
                        key={i}
                        className={`${buttonBaseStyles} ${
                            typeof page === 'string'
                                ? 'cursor-default'
                                : page === currentPage
                                    ? activePageStyles
                                    : pageButtonStyles
                        } rounded-md`}
                        onClick={() => typeof page === 'number' && pageChanged(page)}
                        disabled={typeof page === 'string'}
                    >
                        {page}
                    </button>
                ))}
            </div>

            <button
                className={`${buttonBaseStyles} ${navigationButtonStyles} rounded-r-lg ${currentPage === pageCount ? disabledStyles : ''}`}
                onClick={() => pageChanged(currentPage + 1)}
                disabled={currentPage === pageCount}
            >
                Next
            </button>
        </nav>
    )
}



