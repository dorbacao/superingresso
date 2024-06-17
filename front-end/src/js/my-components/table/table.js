import answer from "../answer";

const tableEvent = new Event('tableEvent');


const templateTeste = ()=>{
    return /*html*/`<div class="flex flex-col items-center justify-between gap-y-4 md:flex-row">
        <p class="text-xs font-normal text-slate-400">Showing 1 to 10 of 50 result</p>
        <!-- Pagination -->
        <nav class="pagination">
          <ul class="pagination-list">
            <li class="pagination-item">
              <a class="pagination-link pagination-link-prev-icon">
                <i data-feather="chevron-left" class="fa-solid fa-chevron-left" width="1em" height="1em"></i>
              </a>
            </li>
            <li class="pagination-item active">
              <a class="pagination-link">1</a>
            </li>           
            <li class="pagination-item">
              <a class="pagination-link pagination-link-next-icon" >
                <i data-feather="chevron-right" class="fa-solid fa-chevron-right" width="1em" height="1em"></i>
              </a>
            </li>
          </ul>
        </nav>
      </div>`;
};

const pageLeftTemplate = (previosIndex, paginate) => {
    
    return /*html*/`
    <li class="pagination-item">
    <a style="${previosIndex < 1 ? 'pointer-events: none;cursor: default;' : ''}" onclick="paging.paginate(${previosIndex},${paginate.pageSize})" class="pagination-link pagination-link-prev-icon" href="#">
        <i data-feather="chevron-left" class="fa-solid fa-chevron-left"  width="1em" height="1em"></i>
    </a>
    </li>
    `;
};

const currentPageTemplate = (currentIndex) =>{
    return /*html*/`
        <li class="pagination-item active">
            <a class="pagination-link" href="#">${currentIndex}</a>
        </li>`;
};

const nextPageTemplate = (nextIndex,totalPage, paginate) => {
    return /*html*/`
        <li class="pagination-item">
        <a style="${nextIndex > totalPage ? 'pointer-events: none;cursor: default;' : ''}" onclick="paging.paginate(${nextIndex},${paginate.pageSize})" class="pagination-link pagination-link-next-icon" href="#">
            <i data-feather="chevron-right" class="fa-solid fa-chevron-right" width="1em" height="1em"></i>
        </a>
        </li>`;
};

const pagingContainerTemplate = (pages, paginate, totalPage) => {
    return /*html*/`
    <div class="flex flex-col items-center justify-between gap-y-4 md:flex-row">
    <p class="text-xs font-normal text-slate-400">Página ${paginate.pageIndex} de ${totalPage} (${paginate.totalCount} linhas encontradas)</p>
    <!-- Pagination -->
    <nav class="pagination">
      <ul class="pagination-list">
     
        ${pages}
      
      </ul>
    </nav>
  </div>
`;
};

const table = {
    options: {},
    init() {

    },
    _renderColumns(options, tr) {
        var index = -1;
        options.columns.forEach(column => {
            index++;

            var th = $.create('th');

            if(column.sort){
                var a = $.create('a');

                th.appendChild(a);
    
                if(column.sort.asc == undefined){
                    //Não atribui classe para negritar o ícone de ordenação
                }else if(column.sort.asc){
                    th.classList.add('datatable-ascending');
                }else{
                    th.classList.add('datatable-descending');
                }                
    
                th.setAttribute('data-sortable', 'true');
    
                a.classList.add('datatable-sorter');
                a.href = '#';
                a.innerHTML = column.title;
                a.dataset.custom = index;
                a.addEventListener('click', async e=>{
                    console.log(e.target.dataset);
                    var allThColumns = e.target.parentNode.parentNode.querySelectorAll('th');

                    var currentTh = allThColumns[e.target.dataset.custom];                    
                    var column = table.options.columns[e.target.dataset.custom];

                    var newSortDirection = ''                    
                    var ascending = 1;
                    var descending = 2;
                    var nondefined = 0;
                    var direction = nondefined;
                    if(currentTh.classList.contains('datatable-ascending')){
                        direction = descending;
                        newSortDirection = 'datatable-descending';
                    }else if(currentTh.classList.contains('datatable-descending')){
                        newSortDirection = '';
                        direction = nondefined;
                    }else{
                        newSortDirection = 'datatable-ascending';
                        direction = ascending;
                    }

                    allThColumns.forEach(innerTh=>{
                        innerTh.classList.remove('datatable-ascending');
                        innerTh.classList.remove('datatable-descending');
                    });

                    if(direction != 0){
                        currentTh.classList.add(newSortDirection);
                    }                    

                    table.options.paging.fieldName = column.sort.field;
                    table.options.paging.direction = direction;
                    console.log(JSON.stringify(table.options.paging));
                    await table.refreshAsync();
                    
                });

            }else{
                th.innerHTML = column.title;
            }            

            if (column.class) {
                column.class.split(' ').forEach((cls) => {
                    th.classList.add(cls);
                });
            }

            

            tr.appendChild(th);
        });
    },
    _validateOptions(options) {
        if (!options) {
            answer.danger('options não pode ser undefined para executar o método render da classe table');
            return false;
        }
        options.id = options.id ?? 'data-table';
        options.class = options.class ?? 'table';
        if (!options.columns) {
            answer.danger('Columns não pode ser undefined');
            return false;
        }
        if (!options.table) {
            answer.danger('table não pode ser undefined');
            return false;
        }
        var tableElement = $.id(options.id);
        if (!tableElement) {
            answer.danger(`Não foi possível encontrar a div com o id ${options.id}`);
            return false;
        }
        return tableElement;
    },
    _renderBody(tbody, data, rowTemplateMethod) {
        data.forEach(row => {
            tbody.innerHTML += rowTemplateMethod(row);
        });

        let evt = this.options.events;
        if (evt) {
            evt.forEach(e => {
                if (!e.type) answer.showError('Atributo type do evento não pode ser undefined');
                if (!e.name) answer.showError('Informe o nome do evento');
                let tags = tbody.querySelectorAll(e.type);
                tags.forEach(tag => {
                    tag.addEventListener(e.name, innerEvent => {
                        const tableChangeEvent = new CustomEvent('tableChange', {
                            detail: {
                                targetEvent: innerEvent,
                                dataset: innerEvent.currentTarget.dataset
                            }
                        });

                        this.getTable().dispatchEvent(tableChangeEvent);
                    });
                });

            });
        }
    },
    _renderPaging(paginate) {

        if (!paginate) return;

        var pageOptions = {
            id:this.options.id,
            paginate(pageIndex, pageSize) {
                const changePage = new CustomEvent('changepage', {
                    detail: { newPage: { pageIndex, pageSize } }
                });
                
                $.id(this.id)
                .querySelector('table')
                .dispatchEvent(changePage)
                ;
            }
        };

        window.paging = pageOptions;

        let totalPage = Math.round(paginate.totalCount / paginate.pageSize);
        let previosIndex = paginate.pageIndex - 1;
        let nextIndex = paginate.pageIndex + 1;
        let currentIndex = paginate.pageIndex;

        let fullHtml = '';

        //Previus page
        fullHtml += pageLeftTemplate(previosIndex, paginate);

        //Current page
        fullHtml += currentPageTemplate(currentIndex);

        //Next page
        fullHtml += nextPageTemplate(nextIndex,totalPage, paginate);

        var html = pagingContainerTemplate(fullHtml, paginate, totalPage);

        $.id(this.options.paging.id).innerHTML = html;
    },
    getTable() {
        return $.id(this.options.id).querySelector('table');
    },
    async fillAsync(fetchPromisse, rowTemplateMethod) {
        this.rowTemplateMethod = rowTemplateMethod;
        this.fetchPromisse = fetchPromisse;
        
        return await this.refreshAsync();
    },
    async refreshAsync() {
        var tbody = $.id(this.options.id).querySelector('tbody');        
        tbody.innerHTML = '';

        var response = await this.fetchPromisse(this.options.paging);
        var data = response.value;

        if (data.length) {
            

            if (!tbody) {
                answer.showError(`Tbody não encontrado dentro da div container ${this.options.id}`);
                return;
            }            

            this._renderBody(tbody, data, this.rowTemplateMethod);

            this._renderPaging(response);
        }
    },
    render(options) {        
        this.options = options;
        $.id(this.options.id).innerHTML = '';
        var element = this._validateOptions(options);
        var table = $.create('table');

        (options.table.class ?? 'table').split(' ').forEach(cls=>{
            table.classList.add(cls);
        });
        
        var thead = $.create('thead');
        
        var tr = $.create('tr');

        thead.appendChild(tr);

        this._renderColumns(options, tr);

        table.appendChild(thead);
        table.appendChild($.create('tbody'));
        element.appendChild(table);

        return this;
    }
}

export default table;


/*
  <div class="table-responsive whitespace-nowrap rounded-primary">
        <table id="users-table" class="table">
          <thead>
            <tr>              
              <th class="w-[35%] uppercase">Usuário</th>
              <th class="w-[15%] uppercase">Email</th>
              <th class="w-[15%] uppercase">Telefone</th>
              <th class="w-[15%] uppercase">Ingresso</th>
              <th class="w-[15%] uppercase">Status</th>
              <th class="w-[5%] !text-right uppercase">Actions</th>
            </tr>
          </thead>
          <tbody>
            
          </tbody>
        </table>
      </div>
*/