<script setup>
import { todosService } from "../../services/TodosService.js";
import { logger } from "../../utils/Logger.js";
import { Todo } from "../../models/Todo.js";
import { ref } from "vue";
import Pop from "../../utils/Pop.js";

const props = defineProps({
  todo: { type: Todo, required: true }
})

const todoId = ref(props.todo.id);

async function deleteTodo() {
  try {
    if (await Pop.confirm("Are you sure you want to delete this todo?", "⚠️ This process cannot be undone", "Yes ✓")) {
      await todosService.deleteTodo(todoId.value)
    }
  } catch (error) {
    logger.error(error);
  }
}
</script>

<template>
  <div class="todo-card d-flex justify-content-evenly">
    <div class="todo-title">{{ todo.title }}</div>
    <div class="todo-description">{{ todo.description }}</div>
    <span>
      <i class="mdi mdi-minus selectable"
        title="Delete Todo"
        @click="deleteTodo">
      </i>
    </span>
  </div>
</template>