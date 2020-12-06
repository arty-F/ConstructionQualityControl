import { RegionReadDto } from 'src/app/dtos/region/region-read-dto'

export class CityReadDto
{
    id: number
    name: string
    region: RegionReadDto
}