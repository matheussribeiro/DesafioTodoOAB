import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NzFormTooltipIcon } from 'ng-zorro-antd/form';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { TodoRequest } from 'src/app/interface/TodoRequest';
import { TodoServiceService } from 'src/app/services/TodoService.service';

@Component({
  selector: 'app-createtasks',
  templateUrl: './createtasks.component.html',
  styleUrls: ['./createtasks.component.css']
})
export class CreatetasksComponent implements OnInit {

  constructor(private fb: FormBuilder,
     private todoService: TodoServiceService,
     private notification: NzNotificationService,
     private router:Router) { }

  validateForm!: FormGroup;
  captchaTooltipIcon: NzFormTooltipIcon = {
    type: 'info-circle',
    theme: 'twotone'
  };

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      name: [null, [Validators.required, Validators.maxLength(100)]],
      description: [null, [Validators.required,Validators.maxLength(500)]],
    });
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      this.createTodo();
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  createTodo() {
    const todo: TodoRequest = {
      name: this.validateForm.value.name,
      description: this.validateForm.value.description,
    };
    this.todoService.createTodo(todo).subscribe(data => {
      this.notification.success('','Tarefa Criada com Sucesso');
      this.router.navigate(['/pendingtasks']);
    })
  }

}
