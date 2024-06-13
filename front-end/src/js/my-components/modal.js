const newModal = (options) => {

    if (!options) {
        options = {};
    }
    if(!options.targetId){
        toastr.error('A propriedade targetId não foi informada ao invocar o método newModal()');
        return;
    }
    let element = document.getElementById(options.targetId);
    if(!element){
        toastr.error(`Não foi possível encontrar o elemento ${options.targetId}. Não foi possível criar o modal`);
        return;
    }
    options.modalId = options.modalId ?? 'modal-content-id';
    options.title = options.title ?? 'Tem certeza?';
    options.text = options.text ?? 'Confirma que deseja fazer esta operação?';
    options.cancelTitle = options.cancelTitle ?? 'Cancelar';
    options.cancelId = options.cancelId ?? 'cancel-button';
    options.confirmTitle = options.confirmTitle ?? 'Confirmar';
    options.confirmId = options.confirmId ?? 'confirm-button';

    element.innerHTML = /*html*/`
    <div class="modal modal-centered" id="${options.modalId}">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <div class="flex items-center justify-between">
              <h6>${options.title}</h6>
              <button type="button"
                class="btn btn-plain-secondary dark:text-slate-300 dark:hover:bg-slate-700 dark:focus:bg-slate-700"
                data-dismiss="modal">
                <i data-feather="x" width="1.5rem" height="1.5rem"></i>
              </button>
            </div>
          </div>
          <div class="modal-body">
            <p class="text-sm text-slate-500 dark:text-slate-300">
              ${options.text}
            </p>
          </div>
          <div class="modal-footer">
            <div class="flex items-center justify-end gap-4">
              <button type="button" id="${options.cancelId}" class="btn btn-secondary" data-toggle="modal"
                data-dismiss="modal">${options.cancelTitle}</button>
              <button type="button" id="${options.confirmId}" class="btn btn-primary">${options.confirmTitle}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
    `;

    var modalOperations = {
        currentModal:null,
        getModal() {
            return document.getElementById(this.options.modalId);
        },
        getCancelButton() {
            return document.getElementById(this.options.cancelId);
        },
        getConfirmButton() {
            return document.getElementById(this.options.confirmId);
        },
        createNew() {
            if(!this.currentModal){
                this.currentModal = createModal(`#${this.options.modalId}`, {
                    keyboard: true, //Boolean. Default is true
                    backdrop: false, //Boolean | 'static'. Default is true
                });
            }            
            return this.currentModal;
        }
    };

    let result = { ...{options}, ...modalOperations };

    return result;
};

export default newModal;