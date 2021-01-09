import { UserReadDto } from "../user/user-read-dto"

export class WorkOfferReadDto {
    id: number
    worker: UserReadDto
    date: Date
    message: string
}