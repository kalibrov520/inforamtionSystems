import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { Http, Response } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { IDataFeed, DataFeed, DataFeedDetails } from '../models/dataFeed';
import { from } from 'rxjs';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class DataFeedService {

  constructor(private http: Http) {
   
  }

  public getAllDataFeedsInfo(): Observable<IDataFeed[]> {
    return this.http
      .get(API_URL + "/api/datatransformation").pipe(map(response => {
           let dataFeeds = response.json();
           return dataFeeds.map(dataFeed => new DataFeed(dataFeed));
         })).catch(this.handleError);
  }

  public getDataFeedDetails(deploymentId: string): Observable<DataFeedDetails[]> {
    return this.http
      .get(API_URL + "/api/datatransformation/" + deploymentId).pipe(map(response => {
           let dataFeeds = response.json();
           return dataFeeds.map(dataFeed => new DataFeedDetails(dataFeed));
         })).catch(this.handleError);
  }

  public getDataFeedFailesInfo(runId: string): Observable<string[]> {
    return this.http.get(API_URL + "/api/datatransformation/datafeedfailes/" + runId).pipe(map(response => {
      let data = response.json();
      return data;
    })).catch(this.handleError);
  }

  private handleError (error: Response | any) {
    console.error('ApiService::handleError', error);
    return Observable.throw(error);
  }

  public startProcess(processDefinitionId: string): Observable<Response>{
    return this.http.post(API_URL + "/api/datatransformation/startprocess/" + processDefinitionId, null);
  }
}