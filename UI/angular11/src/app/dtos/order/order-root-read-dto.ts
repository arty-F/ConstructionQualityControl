import { CityReadDto } from "../city/city-read-dto"
import { UserReadDto } from "../user/user-read-dto"
import { WorkOfferReadDto } from "../work-offer/work-offer-read-dto"

export class OrderRootReadDto {
    id: number
    creationDate: Date
    user: UserReadDto
    demands: string
    postPaid: number
    isCompleted: boolean
    isStarted: boolean
    city: CityReadDto
    workOffers: WorkOfferReadDto[]
}