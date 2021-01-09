import { UserReadDto } from "../user/user-read-dto"
import { WorkOfferReadDto } from "../work-offer/work-offer-read-dto"

export class OrderRootReadDto {
    id: number
    creationDate: Date
    user: UserReadDto
    demands: string
    isCompleted: boolean
    isStarted: boolean
    workOffers: WorkOfferReadDto
}