CREATE OR REPLACE FUNCTION GetNumero (v_num_carta INT4) RETURNS VARCHAR AS $$
DECLARE
    num_carta   VARCHAR;
BEGIN

    CASE
        WHEN v_num_carta < 10 THEN num_carta := v_num_carta::VARCHAR;
        WHEN v_num_carta = 10 THEN num_carta := 'T';
        WHEN v_num_carta = 11 THEN num_carta := 'J';
        WHEN v_num_carta = 12 THEN num_carta := 'Q';
        WHEN v_num_carta = 13 THEN num_carta := 'K';
        WHEN v_num_carta = 14 THEN num_carta := 'A';
        ELSE 
            RAISE EXCEPTION 'Numero de carta nao identificado.';
    END CASE;

    RETURN num_carta;

END;
$$
LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION realizaCargaProbabilidade (v_num_carta_1 INT4, v_num_carta_2 INT4, v_off_or_suited VARCHAR, v_val_prob NUMERIC) RETURNS VOID AS $$
declare
	num_carta_1 		VARCHAR(1);
	num_carta_2 		VARCHAR(1);
	naipe_1				VARCHAR(1);
	naipe_2				VARCHAR(1);
	ds_mao				VARCHAR(200);
begin
	ds_mao := '';
	naipe_1 := '0';
	naipe_2	:= '0';	

	if v_off_or_suited = 'O' then
		naipe_2 := '1';	
	end if;

	insert into probabilidade.tb_probabilidade_mao_vencer (ds_jogo_mao, val_prob_vencer) values 
	(
		GetNumero(v_num_carta_1) || naipe_1 || ' ' || GetNumero(v_num_carta_2) || naipe_2,
		v_val_prob
	);

	return;
END
$$
LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION realizaCarga() RETURNS void AS $$
DECLARE
    v_cursor refcursor;
    v_chave_primaria bigint;
    v_contador_inserts integer;
    v_linha record;
   	v_grau integer;
   	v_query   VARCHAR;
BEGIN
    RAISE NOTICE 'Executando a carga';

   	v_query := 'SELECT * FROM probabilidade.mao_duas_cartas';
   
  -- Escolher o Grau.
  -- v_grau := 1;

  -- v_query := replace(v_query::varchar, '?', v_grau::varchar);

    -- Avança a sequence para o próximo numero e armazena o valor em v_chave_primaria.
    -- SELECT nextval('client.sq_tb_competencia_cl_assunto') INTO v_chave_primaria;

    --Incializa o contador de linhas;
    v_contador_inserts := 0;

    --Executa os inserts
    OPEN v_cursor FOR EXECUTE v_query;
    LOOP FETCH v_cursor INTO v_linha;
    EXIT WHEN NOT FOUND;

        perform realizaCargaProbabilidade(v_linha.numero_carta_1, v_linha.numero_carta_2, v_linha.offorsuited, v_linha.probabilidade_vitoria);
        v_contador_inserts := v_contador_inserts + 1;

    END LOOP;
    CLOSE v_cursor;
   	RAISE NOTICE 'Quantidade items preenchidos: %s', v_contador_inserts;
END
$$
LANGUAGE plpgsql;

select realizaCarga();
