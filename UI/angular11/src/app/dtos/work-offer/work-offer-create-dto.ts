import { UserReadDto } from "../user/user-read-dto"

export class WorkOfferCreateDto {
    worker: UserReadDto
    message: string
}