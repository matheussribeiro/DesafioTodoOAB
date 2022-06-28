import { Component, OnInit } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Todo } from 'src/app/interface/Todo';
import { TodoServiceService } from 'src/app/services/TodoService.service';

@Component({
  selector: 'app-completetasks',
  templateUrl: './completetasks.component.html',
  styleUrls: ['./completetasks.component.css']
})
export class CompletetasksComponent implements OnInit {

  constructor(private todoService : TodoServiceService,
    private notification: NzNotificationService,
    private modal: NzModalService) { }


  TodoList : Todo[] = []
  ngOnInit(): void {
    this.getCompleteTodos();
  }

  getCompleteTodos() {
    this.todoService.getCompleteTodos().subscribe(data => {
      this.TodoList = data;
    }
  )};

  public deleteTodo(id: number): void {
    this.todoService.deleteTodo(id).subscribe(() =>{
      this.notification.success('','Tarefa Removida com Sucesso');
      this.getCompleteTodos();
    })
  }

  public updateStatus(id: number): void {
    let status = false;
    this.todoService.updateStatus(id, status).subscribe(() =>{
      this.notification.success('','Status pendente atualizado com Sucesso');
      this.getCompleteTodos();
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
