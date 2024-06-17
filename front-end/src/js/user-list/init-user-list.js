import toastr from "./../components/toast";
import paging from "../my-components/table/paging";
import myTable from "../my-components/table/table";
import 'toastr/build/toastr.min.css';
import { userService } from '../services/userService';
import answer from "../my-components/answer";

var tableRef;

const template = (row) => /*html*/ `<tr>
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

$bus.subscribe('user-list-loaded', (data) => {
  $.id('spinner').classList.add('hidden');
  $.id('container').classList.remove('hidden');
});

let options = {
  id: 'table-container',
  paging: {
    pageIndex: 1,
    pageSize: 5,
    id: 'pagination-table'
  },
  events: [
    { name: 'click', type: 'button' }
  ],
  table: {
    class: 'table datatable-table'
  },
  columns: [
    { title: 'Usuário', class: 'w-[35%] uppercase', sort:{field:'Nome', asc:true} },
    { title: 'Email', class: 'w-[15%] uppercase', sort:{field:'Email'}},
    { title: 'Telefone', class: 'w-[15%] uppercase' , sort:{field:'Telefone'}},
    { title: 'Ingresso', class: 'w-[15%] uppercase' , sort:{field:'DataInclusao'}},
    { title: 'Status', class: 'w-[15%] uppercase' , sort:{field:'Ativo'}},
    { title: 'Actions', class: 'w-[5%] uppercase' },
  ]
};

async function onChangePage(e) {
  await refreshTable(e.detail.newPage);
}

async function refreshTable(newPage){
  var service = new userService();
  tableRef.options.paging.pageIndex = newPage.pageIndex;
  tableRef.options.paging.pageSize = newPage.pageSize;
  await tableRef.fillAsync(service.getAllAsync, template);
}

async function renderTable(pagination) {

  if (pagination) {
    options.paging.pageIndex = pagination.pageIndex;
    options.paging.pageSize = pagination.pageSize;
  }

  tableRef = myTable.render(options);

  tableRef.getTable().addEventListener('tableChange', event => {
    window.location = "profile.html?id=" + event.detail.dataset.id;
  });

  await refreshTable(options.paging);

  tableRef.getTable().addEventListener('changepage', onChangePage);

  $bus.publish('user-list-loaded');
}

export async function initUserList() {

  var pesquisarButton = document.getElementById('pesquisar-button');

  pesquisarButton.addEventListener('click', async () => {
    await renderTable();
  });

  document.getElementById('exportButton').addEventListener('click', async () => {
    toastr.info('exportButton click');
  });

  pesquisarButton.click();
}

await initUserList();  
