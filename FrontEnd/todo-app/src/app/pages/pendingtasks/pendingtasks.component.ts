import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Todo } from 'src/app/interface/Todo';
import { TodoServiceService } from 'src/app/services/TodoService.service';

@Component({
  selector: 'app-pendingtasks',
  templateUrl: './pendingtasks.component.html',
  styleUrls: ['./pendingtasks.component.css']
})
export class PendingtasksComponent implements OnInit {

  constructor(private todoService : TodoServiceService,
    private notification: NzNotificationService,
    private modal: NzModalService) { }


  TodoList : Todo[] = []

  ngOnInit(): void {
    this.getPendingTodos();
  }

  getPendingTodos() {
    this.todoService.getPendingTodos().subscribe(data => {
      this.TodoList = data;
    }
  )};

  public deleteTodo(id: number): void {
    this.todoService.deleteTodo(id).subscribe(() =>{
      this.notification.success('','Tarefa Excluída com Sucesso');
      this.getPendingTodos();
    })
  }

  public updateStatus(id: number): void {
    let status = true;
    this.todoService.updateStatus(id, status).subscribe(() =>{
      this.notification.success('','Tarefa Concluída com Sucesso');
      this.getPendingTodos();
    })
  }

  showDeleteConfirm(id: number): void {
    this.modal.confirm({
      nzTitle: 'Confirmar exclusão',
      nzContent: '<b style="color: red;">Essa ação não poderá ser desfeita</b>',
      nzOkText: 'Confirmar',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzOnOk: () => this.deleteTodo(id),
      nzCancelText: 'Cancelar',
    });
  }
}
