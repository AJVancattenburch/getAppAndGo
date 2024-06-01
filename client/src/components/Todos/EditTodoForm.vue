<script setup>
import { todosService } from "../../services/TodosService.js";
import { logger } from "../../utils/Logger.js";
import { Todo } from "../../models/Todo.js";
import { watch, ref } from "vue";

const props = defineProps({
  todo: { type: Todo, required: true }
});

const todo = ref(props.todo);

const editableTodo = ref({
  title: props.todo.title,
  description: props.todo.description,
  completed: props.todo.completed
});

const editing = ref(false);

async function editTodo() {
  try {
    await todosService.editTodo(todo.value.id, editableTodo.value);
    todo.value = editableTodo.value;
    editing.value = false;
  } catch (error) {
    logger.error(error);
  }
}
</script>

<template>
  <form class="col-12 todo-card d-flex justify-content-between" @submit.prevent="editTodo">
    <div class="col-3 input-container title-input mb-3">
      <input id="title" name="title" v-model="editableTodo.title" type="text" placeholder=" " class="input" minLength="3" maxLength="50" required />
      <div class="cut"></div>
      <label for="title" class="placeholder">Title...</label>
    </div>
    <div class="col-8 input-container description-input mb-3">
      <input id="description" name="description" v-model="editableTodo.description" type="text" placeholder=" " class="input" minLength="10" maxLength="250" required />
      <div class="cut-long"></div>
      <label for="description" class="placeholder">Description...</label>
    </div>
    <span class="col-1 d-flex flex-column align-items-center">
      <button class="mdi mdi-check selectable"
        type="submit"
        title="Save Changes">
      </button>
      <i class="mdi mdi-close selectable"
        title="Cancel"
        @click="editing = false">
      </i>
    </span>
  </form>
</template>

<style scoped lang="scss">
.input-container > input {
  width: 100%;
}
</style>