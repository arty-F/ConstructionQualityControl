export class OrderCreateDto {
    user: string
    demands: string
    isRoot: boolean
    subOrders: OrderCreateDto[]
}