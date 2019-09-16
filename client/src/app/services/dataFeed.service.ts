import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { IDataFeed, DataFeed } from '../models/dataFeed';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class DataFeedService {
  constructor(private http: Http) {}

  public getAllDataFeedsInfo(): Observable<IDataFeed[]> {
    return this.http
      .get(API_URL + "/api/datatransformation")
      .map(response => {
        const dataFeeds = response.json();
        return dataFeeds.map(dataFeed => new DataFeed(dataFeed));
      })
      .catch(this.handleError);
  }

  private handleError (error: Response | any) {
    console.error('ApiService::handleError', error);
    return Observable.throw(error);
  }
}