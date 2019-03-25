var unidadesSelecionadas = [];
var listaUnidades = [];
var valorTotal = 0;

function GeraValorTotal(valorTotal) {
    $("#divValorTotal").html('<p>Valor total: R$ ' + valorTotal.toLocaleString('pt-BR') + ' </p>');
}

function GeraTabelaUnidadesSelecionadas() {
    var tabelaSelecionadas = '';

    console.log(unidadesSelecionadas);

    for (i = 0; i < unidadesSelecionadas.length; i++) {
        var uni = listaUnidades.find(x => x.id === unidadesSelecionadas[i]);

        tabelaSelecionadas += '<tr>' +
                                        '<td>' + uni.nome + ' - ' + uni.empreendimento + '</td>' +
                                        '<td><button onclick="ExcluiUnidadeSelecionada(' + uni.id + ')" type="button">Excluir</button></td>' +
                                '</tr>';
    }

    $("#tabelaUnidadesSelecionadas").html(tabelaSelecionadas);
}


function SelecionaUnidade(id, empreendimento) {

    if (unidadesSelecionadas.find(x => x == id) == null) {
        var unidadeSelecionada = listaUnidades.find(x => x.id == id);

        unidadesSelecionadas.push(id);

        valorTotal += unidadeSelecionada.valor;

        GeraValorTotal(valorTotal);

        //GeraTabelaUnidadesSelecionadas();

        var tabelaSelecionadas = '<tr>' +
                                        '<td>' + unidadeSelecionada.nome + ' - ' + unidadeSelecionada.empreendimento + '</td>' +
                                        '<td><button onclick="Excluir(this, ' + unidadeSelecionada.id+ ');" type="button">Excluir</button></td>' +
                                '</tr>';

        $('#tabelaUnidadesSelecionadas').append(tabelaSelecionadas);
    }

    


}

function Excluir(botao, id) {
    unidadesSelecionadas.pop(id);

    var unidadeSelecionada = listaUnidades.find(x => x.id == id);

    valorTotal -= unidadeSelecionada.valor;


    GeraValorTotal(valorTotal);

    var tr = $(botao).closest('tr');
    tr.css("background-color", "#FF3700");

    tr.fadeOut(400, function () {
        tr.remove();
    });
    return false;
}




function GeraTabelaUnidades(unidades) {
    var tabela = '';

    //for (var unidade in unidades) {
    //    if (listaUnidades.find(x => x.id == unidade.id) == null) {
    //        listaUnidades.push(unidade);
    //    }

        
    //}
    listaUnidades=  listaUnidades.concat(unidades);

    tabela += '<table class="display" width="100%"  cellspacing="0" name="example" id="tabelaUnidades">' +
                        '<thead>' +
                            '<tr>' +
                                '<th style="text-align: left;">Nome Unidade</th>' +
                                '<th>Andar</th>' +
                                '<th>Bloco</th>' +
                                '<th>Valor R$</th>' +
                                '<th></th>' +
                            '</tr>' +
                        '</thead>' +
                        '<tbody>';

    for (i = 0; i < unidades.length; i++) {
        tabela += '<tr>' +
            '<td>' + unidades[i].nome + '</td>' +
            '<td>' + unidades[i].andar + '</td>' +
            '<td>' + unidades[i].bloco + '</td>' +
            '<td>' + unidades[i].valor.toLocaleString('pt-BR') + '</td>' +
            '<td><button type="button" onclick="SelecionaUnidade(' + unidades[i].id + ');">Selecionar</button></td>' +
            '</tr>';
    }

                                
   tabela +=            '</tbody>' +
                '</table>';

   return tabela;
}