import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Todo } from 'src/app/interface/Todo';
import { TodoRequest } from 'src/app/interface/TodoRequest';
import { TodoServiceService } from 'src/app/services/TodoService.service';
import {Location} from '@angular/common';

@Component({
  selector: 'app-updatetasks',
  templateUrl: './updatetasks.component.html',
  styleUrls: ['./updatetasks.component.css']
})
export class UpdatetasksComponent implements OnInit {


  constructor(private fb: FormBuilder,
    private todoService: TodoServiceService,
    private notification: NzNotificationService,
    private router:Router,
    private route: ActivatedRoute,
    private location: Location) { }

 validateForm!: FormGroup;
 todo!: Todo;

 ngOnInit(): void {
  const idString = this.route.snapshot.paramMap.get('id');
  const id = Number(idString);
  this.todoService.getById(id).subscribe(data => {
    this.todo = data;
    console.log(this.todo);
  })

   this.validateForm = this.fb.group({
     name: [null, [Validators.required, Validators.maxLength(100)]],
     description: [null, [Validators.required,Validators.maxLength(500)]],
   });
 }

 submitForm(): void {
   if (this.validateForm.valid) {
     this.updateTodo();
   } else {
     Object.values(this.validateForm.controls).forEach(control => {
       if (control.invalid) {
         control.markAsDirty();
         control.updateValueAndValidity({ onlySelf: true });
       }
     });
   }
 }

 updateTodo() {
   const todo: Todo = {
    name: this.validateForm.value.name,
    description: this.validateForm.value.description,
    id: this.todo.id,
    isComplete: this.todo.isComplete,
    dtCreated: this.todo.dtCreated
   };
   this.todoService.updateTodo(todo).subscribe(data => {
     this.notification.success('','Tarefa Atualizada com Sucesso');
     this.location.back();
   })
 }

  cancel() {
    this.location.back();
  }
}
