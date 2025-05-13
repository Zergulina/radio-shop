export interface Product {
    id: number,
    name: string,
    description: string,
    price: number,
    rating: number,
    orderAmount: number,
    tags: Tag[],
    imageId?: string
}

export interface CreateProduct {
    name: string,
    description: string,
    price: number,
    imageFile: File | null
}

export interface UpdateProduct {
    name: string,
    description: string,
    price: number
}

export interface Tag {
    id: number,
    name: string
}

export interface CreateTag {
    name: string,
}

export interface UpdateTag {
    name: string,
}