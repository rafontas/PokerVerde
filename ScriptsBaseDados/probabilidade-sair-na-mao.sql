--
-- Probabilidade do jogo sair na mão em porcentagem;
--

UPDATE probabilidade.mao_duas_cartas
SET probabilidade_sair=0.301659125188537
WHERE offorsuited = 'S';

UPDATE probabilidade.mao_duas_cartas
SET probabilidade_sair=0.9049773755656109
WHERE offorsuited = 'O' and
numero_carta_1 <> numero_carta_2 ;

UPDATE probabilidade.mao_duas_cartas
SET probabilidade_sair=0.4524886877828054
WHERE offorsuited = 'O' and
numero_carta_1 = numero_carta_2 ;

--
-- Quantidade de jogos no baralho
--
UPDATE probabilidade.mao_duas_cartas
SET numero_jogos=4
WHERE offorsuited = 'S';

UPDATE probabilidade.mao_duas_cartas
SET numero_jogos = 12
WHERE offorsuited = 'O' and
numero_carta_1 <> numero_carta_2;

UPDATE probabilidade.mao_duas_cartas
SET numero_jogos = 6
WHERE offorsuited = 'O' and
numero_carta_1 = numero_carta_2 ;