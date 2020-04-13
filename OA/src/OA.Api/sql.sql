
    
alter table authority_operator_info  drop foreign key FK_3C88F528


    
alter table bring_up_person_info  drop foreign key FK_CD3DD8BF


    
alter table bring_up_person_info  drop foreign key FK_9263C8BF


    
alter table famous_race_info  drop foreign key FK_4FE355D8


    
alter table module_info  drop foreign key FK_4AD05821


    
alter table module_info  drop foreign key FK_ECC456FD


    
alter table person_info  drop foreign key FK_2705EE8F


    
alter table reawrds_and_punishment_info  drop foreign key FK_C0D3620E


    
alter table reckoning_info  drop foreign key FK_4CCDC6C7


    
alter table reckoning_info  drop foreign key FK_D04BF97E


    
alter table reckoning_list_info  drop foreign key FK_C08A84BA


    
alter table reckoning_list_info  drop foreign key FK_CF851211


    
alter table record_info  drop foreign key FK_24388FAB


    
alter table role_info  drop foreign key FK_EB97EDA1


    
alter table role_info  drop foreign key FK_7C292593


    
alter table time_card_nfo  drop foreign key FK_E80100D


    
alter table time_card_nfo  drop foreign key FK_5EC6A9B0


    
alter table time_card_nfo  drop foreign key FK_645E74F8


    
alter table user_info  drop foreign key FK_E3C55ED3


    drop table if exists account_item_info

    drop table if exists authority_operator_info

    drop table if exists bring_up_content_info

    drop table if exists bring_up_person_info

    drop table if exists duty_info

    drop table if exists famous_race_info

    drop table if exists module_info

    drop table if exists person_info

    drop table if exists reawrds_and_punishment_info

    drop table if exists reckoning_info

    drop table if exists reckoning_list_info

    drop table if exists reckoning_name_info

    drop table if exists record_info

    drop table if exists role_info

    drop table if exists time_card_nfo

    drop table if exists user_info

    create table account_item_info (
        id INTEGER not null,
       name VARCHAR(20),
       type VARCHAR(4),
       utit VARCHAR(20),
       create_date DATETIME,
       update_date DATETIME,
       primary key (id)
    )

    create table authority_operator_info (
        id INTEGER not null,
       `table` VARCHAR(50),
       entity_type VARCHAR(50) not null,
       add_falg INTEGER,
       edit_falg INTEGER,
       delete_falg INTEGER,
       selete_falg INTEGER,
       create_date DATETIME,
       update_date DATETIME,
       role_id INTEGER,
       primary key (id)
    )

    create table bring_up_content_info (
        id INTEGER not null,
       name VARCHAR(50),
       content VARCHAR(100),
       start_date DATETIME,
       end_date DATETIME,
       unit VARCHAR(50),
       lecturer VARCHAR(50),
       place VARCHAR(100),
       create_date DATETIME,
       update_date DATETIME,
       primary key (id)
    )

    create table bring_up_person_info (
        id INTEGER not null,
       score INTEGER,
       up_to_grate VARCHAR(2),
       remark VARCHAR(200) not null,
       create_date DATETIME,
       update_date DATETIME,
       bring_up_content_id INTEGER,
       record_id INTEGER,
       primary key (id)
    )

    create table duty_info (
        id INTEGER not null,
       accession_date DATETIME,
       dimission_date DATETIME,
       dimission_reason VARCHAR(255),
       first_pact_date DATETIME,
       pact_start_date DATETIME,
       pact_end_date DATETIME,
       bank_name VARCHAR(50),
       bank_id VARCHAR(20),
       society_safefy_no VARCHAR(20),
       annuity_safefy_no VARCHAR(20),
       dole_safefy_no VARCHAR(20),
       medicare_safefy_no VARCHAR(20),
       compo_safefy_no VARCHAR(20),
       acoumulation_fund_no VARCHAR(20),
       create_date DATETIME,
       update_date DATETIME,
       primary key (id)
    )

    create table famous_race_info (
        id INTEGER not null,
       name VARCHAR(50) not null,
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       primary key (id)
    )

    create table module_info (
        id INTEGER not null,
       name VARCHAR(50) not null,
       href VARCHAR(100),
       create_date DATETIME,
       update_date DATETIME,
       parent_id INTEGER,
       user_id INTEGER,
       primary key (id)
    )

    create table person_info (
        id INTEGER not null,
       qq VARCHAR(15),
       eamil VARCHAR(20),
       handset VARCHAR(15),
       telphone VARCHAR(15),
       address VARCHAR(100),
       postlacode VARCHAR(5),
       second_school_age VARCHAR(10),
       second_speciaity VARCHAR(40),
       graduate_school VARCHAR(40),
       graduate_date DATETIME,
       party_member_date DATETIME,
       computer_grate VARCHAR(10),
       likes VARCHAR(50),
       ones_strong_suit VARCHAR(50),
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       primary key (id)
    )

    create table reawrds_and_punishment_info (
        id INTEGER not null,
       type INTEGER,
       reason TEXT,
       content VARCHAR(200),
       money INTEGER,
       start_date DATETIME,
       end_date DATETIME,
       create_date DATETIME,
       update_date DATETIME,
       record_id INTEGER,
       primary key (id)
    )

    create table reckoning_info (
        id INTEGER not null,
       money INTEGER,
       create_date DATETIME,
       update_date DATETIME,
       recore INTEGER,
       account_item_id INTEGER,
       primary key (id)
    )

    create table reckoning_list_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       record_id INTEGER,
       reckoning_name_id INTEGER,
       primary key (id)
    )

    create table reckoning_name_info (
        id INTEGER not null,
       name VARCHAR(20),
       `explain` VARCHAR(200),
       create_date DATETIME,
       update_date DATETIME,
       primary key (id)
    )

    create table record_info (
        id INTEGER not null,
       record_number VARCHAR(5) not null,
       name VARCHAR(10) not null,
       sex VARCHAR(2) not null,
       birthday DATETIME not null,
       card_no VARCHAR(18) not null,
       photo VARCHAR(50) not null,
       marital_status VARCHAR(4) not null,
       address VARCHAR(100) not null,
       postlacode VARCHAR(5) not null,
       party_member VARCHAR(2),
       school_age INTEGER,
       speciaity VARCHAR(10) not null,
       foreign_language VARCHAR(10) not null,
       grate VARCHAR(10) not null,
       famous_race VARCHAR(10) not null,
       native_place VARCHAR(10) not null,
       political_outlook VARCHAR(10) not null,
       education VARCHAR(50) not null,
       major VARCHAR(100) not null,
       employment_form VARCHAR(100),
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       primary key (id)
    )

    create table role_info (
        id INTEGER not null,
       name VARCHAR(50) not null,
       create_date DATETIME,
       update_date DATETIME,
       parent_id INTEGER,
       user_id INTEGER,
       primary key (id)
    )

    create table time_card_nfo (
        id INTEGER not null,
       `explain` VARCHAR(200),
       start_date DATETIME,
       end_date DATETIME,
       ratifier_date DATETIME,
       create_date DATETIME,
       update_date DATETIME,
       record_id INTEGER,
       user_id INTEGER,
       ratifier_record_id INTEGER,
       primary key (id)
    )

    create table user_info (
        id INTEGER not null,
       account VARCHAR(20) not null,
       password VARCHAR(50) not null,
       create_date DATETIME,
       update_date DATETIME,
       role_id INTEGER,
       primary key (id)
    )

    alter table authority_operator_info 
        add index (role_id), 
        add constraint FK_3C88F528 
        foreign key (role_id) 
        references role_info (id)

    alter table bring_up_person_info 
        add index (bring_up_content_id), 
        add constraint FK_CD3DD8BF 
        foreign key (bring_up_content_id) 
        references bring_up_content_info (id)

    alter table bring_up_person_info 
        add index (record_id), 
        add constraint FK_9263C8BF 
        foreign key (record_id) 
        references record_info (id)

    alter table famous_race_info 
        add index (user_id), 
        add constraint FK_4FE355D8 
        foreign key (user_id) 
        references user_info (id)

    alter table module_info 
        add index (parent_id), 
        add constraint FK_4AD05821 
        foreign key (parent_id) 
        references module_info (id)

    alter table module_info 
        add index (user_id), 
        add constraint FK_ECC456FD 
        foreign key (user_id) 
        references user_info (id)

    alter table person_info 
        add index (user_id), 
        add constraint FK_2705EE8F 
        foreign key (user_id) 
        references user_info (id)

    alter table reawrds_and_punishment_info 
        add index (record_id), 
        add constraint FK_C0D3620E 
        foreign key (record_id) 
        references record_info (id)

    alter table reckoning_info 
        add index (recore), 
        add constraint FK_4CCDC6C7 
        foreign key (recore) 
        references record_info (id)

    alter table reckoning_info 
        add index (account_item_id), 
        add constraint FK_D04BF97E 
        foreign key (account_item_id) 
        references account_item_info (id)

    alter table reckoning_list_info 
        add index (record_id), 
        add constraint FK_C08A84BA 
        foreign key (record_id) 
        references record_info (id)

    alter table reckoning_list_info 
        add index (reckoning_name_id), 
        add constraint FK_CF851211 
        foreign key (reckoning_name_id) 
        references reckoning_name_info (id)

    alter table record_info 
        add index (user_id), 
        add constraint FK_24388FAB 
        foreign key (user_id) 
        references user_info (id)

    alter table role_info 
        add index (parent_id), 
        add constraint FK_EB97EDA1 
        foreign key (parent_id) 
        references role_info (id)

    alter table role_info 
        add index (user_id), 
        add constraint FK_7C292593 
        foreign key (user_id) 
        references user_info (id)

    alter table time_card_nfo 
        add index (record_id), 
        add constraint FK_E80100D 
        foreign key (record_id) 
        references record_info (id)

    alter table time_card_nfo 
        add index (user_id), 
        add constraint FK_5EC6A9B0 
        foreign key (user_id) 
        references user_info (id)

    alter table time_card_nfo 
        add index (ratifier_record_id), 
        add constraint FK_645E74F8 
        foreign key (ratifier_record_id) 
        references record_info (id)

    alter table user_info 
        add index (role_id), 
        add constraint FK_E3C55ED3 
        foreign key (role_id) 
        references role_info (id)
