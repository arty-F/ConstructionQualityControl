import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { environment } from "src/environments/environment";
import { CityReadDto } from "../dtos/city/city-read-dto";

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class CityService {

    private _cities = new BehaviorSubject<string[]>(null)
    cities: CityReadDto[]
    citiesReadable: string[]

    constructor(private http: HttpClient) {
        this.http.get<CityReadDto[]>(environment.apiUrl + '/City', httpOptions).subscribe(res => {
            this.cities = res
            this.citiesReadable = this.cities.map(c => c.name + ', ' + c.region.name)
            this._cities.next(this.citiesReadable)
        })
    }

    GetCityList(): Observable<string[]> {
        return this._cities.asObservable()
    }

    ConvertStrToCityDto(str: string): CityReadDto {
        let currentCity = str.split(", ")
        return this.cities.filter(c => c.name == currentCity[0] && c.region.name == currentCity[1])[0]
    }
}