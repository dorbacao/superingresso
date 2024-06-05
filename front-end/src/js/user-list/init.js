import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
import { userService } from '../services/userService';

const template = (row) => `<tr>
<td>
  <div class="flex items-center gap-3">
    <div class="avatar avatar-circle">
      <img class="avatar-img" src="./images/avatar1.png" alt="Avatar 1" />
    </div>
    <div>
      <h6 class="whitespace-nowrap text-sm font-medium text-slate-700 dark:text-slate-100">
        ${row.nome}
      </h6>
      <p class="truncate text-xs text-slate-500 dark:text-slate-400">
        ${row.login}
      </p>
    </div>
  </div>
</td>
<td>${row.email}</td>
<td>${row.telefone}</td>
<td>${new Date(row.dataInclusao).toLocaleDateString('pt-BR')}</td>
<td>
  <div class="badge badge-soft-${row.ativo ? "success" : "danger"}">
    ${row.ativo ? "Ativo" : "Inativo"}
  </div>
</td>
<td>
    <button id="edit-button" data-id="${row.id}" class="btn btn-outline-primary w-full font-medium">        
        <span data-id="${row.id}">Editar</span>
    </button>
</td>
</tr>`;


async function loadTable() {
    var table = document.getElementById('users-table');
    var tBody = table.querySelector('tbody');

    tBody.innerHTML = '';

    var service = new userService();
    var users = await service.getAllAsync();

    users.forEach(row => {
        tBody.innerHTML += template(row);
    });

    var editButtonArray = tBody.getElementsByTagName('button');
    console.log('editButtonArray');
    console.log(editButtonArray.length);

    for (let i = 0; i < editButtonArray.length; i++) {
        const element = editButtonArray[i];
        element.addEventListener('click', event=>{
            var id = event.target.dataset.id;
            window.location = `profile.html?id=${id}`;
        });
    }

}

export async function initUserList() {

    var pesquisarButton =document.getElementById('pesquisar-button');

    pesquisarButton.addEventListener('click', async () => {
        await loadTable();
    });

    document.getElementById('exportButton').addEventListener('click', async () => {
        toastr.info('exportButton click');
    });

    pesquisarButton.click();
}

await initUserList();
