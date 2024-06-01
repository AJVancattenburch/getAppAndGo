<script setup>
import { todosService } from "../../services/TodosService.js";
import { logger } from "../../utils/Logger.js";
import { Todo } from "../../models/Todo.js";
import { ref, watch, watchEffect } from "vue";
import Pop from "../../utils/Pop.js";
import EditTodoForm from "./EditTodoForm.vue";

const props = defineProps({
  todo: { type: Todo, required: true }
})

const todoId = ref(props.todo.id);

const todo = ref(props.todo);

const editing = ref(false);

async function deleteTodo() {
  try {
    if (await Pop.confirm("Are you sure you want to delete this todo?", "⚠️ This process cannot be undone", "Yes ✓")) {
      await todosService.deleteTodo(todoId.value)
    }
  } catch (error) {
    logger.error(error);
  }
}

async function toggleCompleted() {
  todo.value.completed ? !todo.value.completed : todo.value.completed;
  if (todo.value.completed) {
    let todoCard = document.querySelector('.todo-card');
    todoCard.classList.add('completed')
  } else {
    let todoCard = document.querySelector('.todo-card');
    todoCard.classList.remove('completed');
  }
  logger.log(todo.value.completed);
}

</script>

<template>
  <div class="col-12 todo-card d-flex justify-content-between" v-if="!editing">
    <div class="col-1 todo-completed">
      <input type="checkbox" v-model="todo.completed" @change="toggleCompleted" />
    </div>
    <div class="col-2 todo-title">{{ todo.title }}</div>
    <div class="col-8 todo-description" >{{ todo.description }}</div>
    <span class="col-1 d-flex flex-column align-items-center">
      <i class="mdi mdi-pencil selectable"
        title="Edit Todo"
        @click="editing = true">
      </i>
      <i class="mdi mdi-minus selectable"
        title="Delete Todo"
        @click="deleteTodo">
      </i>
    </span>
  </div>
  <div class="col-12" v-else>
    <EditTodoForm :todo="todo" />
  </div>
</template>

<style scoped lang="scss">
.completed {
  text-decoration: line-through !important;
  color: #6c757d !important;
  -webkit-text-fill-color: #864747 !important;
}
</style>