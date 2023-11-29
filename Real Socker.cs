using ConsoleApp9;
using System;

class Programa
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem Vindo ao RealSocker : ");
        Console.WriteLine(" - - - - Escolha : - - - -  ");
        Console.WriteLine("Opção 1 : 1 Player vs Computador ");
        Console.WriteLine("Opção 2 : Player vs Player");
        Console.WriteLine("Digite a sua opção :");
        int opcao = int.Parse(Console.ReadLine());

        AtributoJogador jogador1 = new AtributoJogador();
        AtributoJogador jogador2 = new AtributoJogador();

        if (opcao == 1)
        {
            Console.WriteLine("Digite o nome do Player 1");
            jogador1.Nome = Console.ReadLine();
            jogador2.Nome = "Computador";
            Console.WriteLine("Você irá jogar contra o " + jogador2.Nome);
        }
        else if (opcao == 2)
        {
            Console.WriteLine("Digite o nome do Player 1");
            jogador1.Nome = Console.ReadLine();
            Console.WriteLine("Digite o nome do Player 2");
            jogador2.Nome = Console.ReadLine();
        }
        else
        {
            return;
        }
        jogador1.Energias = 10;
        jogador2.Energias = 10;

        Random randNum = new Random();

        int valor = randNum.Next(1, 3);

        Console.WriteLine("O jogo será iniciado por: " + (valor == 1 ? jogador1.Nome : jogador2.Nome));

        const int GOL = 1;
        const int PENALTI = 2;
        const int FALTA = 3;
        const int CARTAOAMARELO = 4;
        const int CARTAOVERMELHO = 5;
        const int ENERGIA = 6;


        /*
         *
         */

        int somaJogadorUm = 0;
        somaJogadorUm = EfetuarCalculoDaJogada(randNum, somaJogadorUm);

        int pontuacaoTotalJogador1 = 0;
        int pontuacaoTotalJogador2 = 0;

        while (jogador1.Energias > 0 && jogador2.Energias > 0)
        {
            AtributoJogador jogadorAtual = (valor == 1 ? jogador1 : jogador2);

            Console.WriteLine(jogadorAtual.Nome + " aperte enter para receber suas cartas");
            Console.ReadLine();

            int carta1 = randNum.Next(GOL, ENERGIA + 1);
            int carta2 = randNum.Next(GOL, ENERGIA + 1);
            int carta3 = randNum.Next(GOL, ENERGIA + 1);

            Console.WriteLine("Carta 1: " + carta1);
            Console.WriteLine("Carta 2: " + carta2);
            Console.WriteLine("Carta 3: " + carta3);
            
            
            int pontuacao = carta1 + carta2 + carta3;
            Console.WriteLine("A pontuação de " + jogadorAtual.Nome + " é " + pontuacao + " pontos");

            // Adicionando a pontuação da rodada a pontuação total
            jogadorAtual.Pontuacao += pontuacao;

            Console.WriteLine("Pontuação total de " + jogador1.Nome + ": " + pontuacaoTotalJogador1);
            Console.WriteLine("Pontuação total de " + jogador2.Nome + ": " + pontuacaoTotalJogador2);

            //Verifica se houve cartão amerelo 
            if (pontuacao == CARTAOAMARELO * 3)
            {
                Console.WriteLine($"Jogador passou a vez ");
            }

            // Verifica se houve penalidade
            if (pontuacao == FALTA * 3)
            {
                Console.WriteLine($"O {jogadorAtual.Nome} cometeu falta e passou a vez");
            }
            else if (pontuacao == PENALTI * 3)
            {
                Console.WriteLine("Escolha um lado ");
                Console.WriteLine("100 - lado esquerdo rasteiro ");
                Console.WriteLine("101 - lado esquerdo alto");
                Console.WriteLine("102 - meio rasteiro ");
                Console.WriteLine("103 - meio alto ");
                Console.WriteLine("104 - lado direito rasteiro ");
                Console.WriteLine("105 - lado direito alto ");

                int EscolhadeLado = int.Parse(Console.ReadLine());

                // Lógica para determinar se o jogador acertou o lado

                bool jogadorAcertou = (EscolhadeLado == 100 && valor == 1) || (EscolhadeLado == 105 && valor == 2);

                // Lógica para a máquina defender o pênalti

                int ladoDefendidoPelaMaquina = randNum.Next(100, 106);
                bool maquinaDefendeu = ladoDefendidoPelaMaquina == EscolhadeLado;

                if (jogadorAcertou && !maquinaDefendeu)
                {
                    Console.WriteLine($"O jogador {jogadorAtual.Nome} acertou o lado! GOL!");
                    jogadorAtual.Gol += 1;
                    // Jogador fez o gol.
                }
                else if (maquinaDefendeu)
                {
                    Console.WriteLine($"A máquina defendeu o pênalti! {jogadorAtual.Nome} errou o lado. Sem gol.");
                }
                else
                {
                    Console.WriteLine($"O jogador {jogadorAtual.Nome} errou o lado. Sem gol.");
                }
            }

            // Verifica se há cartão vermelho
            if (carta1 == CARTAOVERMELHO && carta2 == CARTAOVERMELHO && carta3 == CARTAOVERMELHO)
            {
                jogadorAtual.Energias -= 2;
                Console.WriteLine($"O jogador {jogadorAtual.Nome} recebeu cartão vermelho. Energia restante: {jogadorAtual.Energias}");
            }

            // Verifica se as cartas são iguais
            if (carta1 == carta2 && carta1 == carta3)
            {
                Console.WriteLine($"Como suas cartas foram iguais, {jogadorAtual.Nome} continua com {jogadorAtual.Energias} energias");
            }
            else
            {
                // Se as cartas são diferentes, jogador perde 1 energia
                jogadorAtual.Energias -= 1;
                Console.WriteLine($"Como suas cartas foram diferentes, você perdeu 1 energia. Energia restante: {jogadorAtual.Energias}");
            }

            // Passa a vez para o próximo jogador
            valor = (valor == 1 ? 2 : 1);
        }

        // Final do jogo
        if (pontuacaoTotalJogador1 > pontuacaoTotalJogador2)
        {
            Console.WriteLine("Parabéns " + jogador1.Nome + " por vencer a partida!");
        }
        else if (pontuacaoTotalJogador1 < pontuacaoTotalJogador2)
        {
            Console.WriteLine("Parabéns " + jogador2.Nome + " por vencer a partida!");
        }
        else
        {
            Console.WriteLine("A partida terminou em empate");
        }

        while (true)
        {
            Console.WriteLine("Digite 'Y' para voltar para o início ou 'S' para sair:");
            string Opcao = Console.ReadLine().ToUpper();

            if (Opcao == "Y")
            {
                // Reiniciar o jogo
            }
            else if (Opcao == "S")
            {
                // Sair do jogo
                Console.WriteLine("Obrigado por jogar! Até a próxima!");
                return;
            }
            else
            {
                // Opção inválida
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }

    private static int EfetuarCalculoDaJogada(Random randNum, int somaJogadorUm)
    {
        for (int i = 0; i < 3; i++)
        {
            int cartaSorteada1 = randNum.Next(1, 6);
            int cartaSorteada2 = randNum.Next(1, 6);
            int cartaSorteada3 = randNum.Next(1, 6);

            if (cartaSorteada1 == 1)
            {
                somaJogadorUm += 3;
            }
            else if (cartaSorteada1 == 2)
            {
                somaJogadorUm += 2;
            }
            else if (cartaSorteada1 == 3)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada1 == 4)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada1 == 5)
            {
                somaJogadorUm += 0;
            }
            else if (cartaSorteada1 == 6)
            {
                somaJogadorUm += 2;
            }
            if (cartaSorteada2 == 1)
            {
                somaJogadorUm += 3;
            }
            else if (cartaSorteada2 == 2)
            {
                somaJogadorUm += 2;
            }
            else if (cartaSorteada2 == 3)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada2 == 4)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada2 == 5)
            {
                somaJogadorUm += 0;
            }
            else if (cartaSorteada2 == 6)
            {
                somaJogadorUm += 2;
            }
            if (cartaSorteada2 == 1)
            {
                somaJogadorUm += 3;
            }
            else if (cartaSorteada3 == 2)
            {
                somaJogadorUm += 2;
            }
            else if (cartaSorteada3 == 3)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada3 == 4)
            {
                somaJogadorUm += 1;
            }
            else if (cartaSorteada3 == 5)
            {
                somaJogadorUm += 0;
            }
            else if (cartaSorteada3 == 6)
            {
                somaJogadorUm += 2;
            }
        }

        return somaJogadorUm;
    }
    


    static AtributoJogador CriarJogador()
    {
        AtributoJogador jogador = new AtributoJogador();

        Console.WriteLine("Entre com o nome do jogador:");
        jogador.Nome = Console.ReadLine();

        jogador.Energias = 10;

        return jogador;
    }
}
