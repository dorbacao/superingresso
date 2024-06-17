


const paging = (paginate) => {

    var options = {
        paginate(pageIndex, pageSize) {
            const changePage = new CustomEvent('changepage', {
                detail:{newPage: {pageIndex, pageSize}}
            });
            document.dispatchEvent(changePage);
        }
    };

    window.paging = options;

    let totalPage = Math.round(paginate.totalCount / paginate.pageSize);
    let previosIndex = paginate.pageIndex - 1;
    let nextIndex = paginate.pageIndex + 1;
    let currentIndex = paginate.pageIndex;

    let fullHtml = '';

    //Previos
    fullHtml += /*html*/`
        <li class="pagination-item">
            <a style="${previosIndex < 1 ? 'pointer-events: none;cursor: default;' : ''}" onclick="paging.paginate(${previosIndex},${paginate.pageSize})" class="pagination-link pagination-link-prev-icon" href="#">
                <i data-feather="chevron-left" width="1em" height="1em"></i>
            </a>
        </li>
    `;

    //Current
    fullHtml += /*html*/`
            <li class="pagination-item active">
              <a class="pagination-link" href="#">${currentIndex}</a>
            </li>
        `;
    
    //Next
    fullHtml += /*html*/`
        <li class="pagination-item">
            <a style="${nextIndex > totalPage ? 'pointer-events: none;cursor: default;' : ''}" onclick="paging.paginate(${nextIndex},${paginate.pageSize})" class="pagination-link pagination-link-next-icon" href="#">
                <i data-feather="chevron-right" width="1em" height="1em"></i>
            </a>
        </li>
    `;

    console.log(paginate);
    var template = /*html*/`
    <div class="flex flex-col items-center justify-between gap-y-4 md:flex-row">
        <p class="text-xs font-normal text-slate-400">Página ${paginate.pageIndex} de ${totalPage} (${paginate.totalCount} linhas encontradas)</p>
        <!-- Pagination -->
        <nav class="pagination">
          <ul class="pagination-list">
         
            ${fullHtml}
          
          </ul>
        </nav>
      </div>
    `;

    return { options, template };

}

export default paging;