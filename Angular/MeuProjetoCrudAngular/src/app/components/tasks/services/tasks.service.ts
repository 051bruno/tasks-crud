import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { TaskModel } from '../Models/TaskModel';
import { RetTaskModel } from '../Models/ret-taskModel';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = this.getApiUrl();
  }

  private getApiUrl(): string {
    
    return environment.apiUrl;
  }

  // Obtém todas as tarefas
  getTasks(): Observable<RetTaskModel> {
    return this.http.get<RetTaskModel>(`${this.apiUrl}/Tasks`);
  }

  // Obtém uma tarefa específica pelo ID
  getTaskById(id: number | undefined): Observable<TaskModel> {
    return this.http.get<TaskModel>(`${this.apiUrl}/Tasks/${id}`);
  }

  // Cria uma nova tarefa
  createTask(task: TaskModel): Observable<TaskModel> {
    return this.http.post<TaskModel>(`${this.apiUrl}/Tasks`, task);
  }  

  // Atualiza uma tarefa existente pelo ID
  updateTask(id: number, task: TaskModel): Observable<TaskModel> {
    return this.http.put<TaskModel>(`${this.apiUrl}/Tasks/${id}`, task);
  }

  // Deleta uma tarefa pelo ID
  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Tasks/${id}`);
  }
}
