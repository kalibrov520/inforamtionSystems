import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { IDataFeed } from "../models/dataFeed";

@Injectable({
  providedIn: "root"
})
export class DataFeedService {
  private dataFeedUrl = "api/dataFeeds/dataFeeds.json";

  constructor(private http: HttpClient) {}

  getData(): IDataFeed[] {
    return [
      {
        status: "failed",
        dataFeed: "State Street Data Feed",
        lastRunning: "02/09/2019",
        successRows: 100,
        failedRows: 2
      },
      {
        status: "failed",
        dataFeed: "Open Finance Data Feed",
        lastRunning: "01/08/2019",
        successRows: 75,
        failedRows: 15
      },
      {
        status: "success",
        dataFeed: "Capital Street Data Feed",
        lastRunning: "03/09/2019",
        successRows: 234,
        failedRows: 0
      }
    ];
  }

  getDataFeeds(): Observable<IDataFeed[]> {
    return this.http.get<IDataFeed[]>(this.dataFeedUrl).pipe(
      tap(data => console.log("All: " + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
