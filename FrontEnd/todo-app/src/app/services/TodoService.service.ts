import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Todo } from '../interface/Todo';
import { TodoRequest } from '../interface/TodoRequest';

@Injectable({
  providedIn: 'root'
})
export class TodoServiceService {


  baseurl = 'https://localhost:5001/api/Todo';

constructor(private http : HttpClient) { }

  getTodos() {
    return this.http.get<Todo[]>(this.baseurl);
  }

  getPendingTodos() {
    return this.http.get<Todo[]>(`${this.baseurl}/pendingtasks`);
  }

  getCompleteTodos() {
    return this.http.get<Todo[]>(`${this.baseurl}/completetasks`);
  }

  createTodo(todo: TodoRequest) {
    return this.http.post<Todo>(this.baseurl, todo);
  }

  deleteTodo(id: number) {
    return this.http.delete(`${this.baseurl}/${id}`,{ responseType: 'text'});
  }

  updateStatus(id: number, status: boolean) {
    return this.http.put<boolean>(`${this.baseurl}/updatestatus/${id}`,status);
  }

  getById(id: number) {
    return this.http.get<Todo>(`${this.baseurl}/${id}`);
  }

  updateTodo(todo: Todo) {
    return this.http.put<Todo>(`${this.baseurl}/${todo.id}`, todo);
  }
}
