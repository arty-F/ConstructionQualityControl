import { CityReadDto } from 'src/app/dtos/city/city-read-dto'

export class UserReadDto {
    id: number
    login: string
    registrationDate: Date
    firstName: string
    lastName: string
    patronymic: string
    companyName: string
    companyDescription: string
    birthDate: Date
    city: CityReadDto
    role: string
}