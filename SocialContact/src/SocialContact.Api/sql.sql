
    
alter table admin_info  drop foreign key fk_admin_role_id


    
alter table admin_info  drop foreign key fk_file_id


    
alter table admin_info  drop foreign key fk_parent_id


    
alter table admin_role_info  drop foreign key fk_admin_role_id


    
alter table admin_role_info  drop foreign key fk_admin_id


    
alter table edution_category_info  drop foreign key fk_admin_id


    
alter table edution_info  drop foreign key fk_admin_id


    
alter table edution_info  drop foreign key fk_edution_category_id


    
alter table file_category_info  drop foreign key fk_admin_id


    
alter table icon_info  drop foreign key fk_admin_id


    
alter table job_category_info  drop foreign key fk_admin_id


    
alter table job_category_info  drop foreign key fk_parent_id


    
alter table like_category_info  drop foreign key fk_admin_id


    
alter table like_category_info  drop foreign key fk_parent_id


    
alter table like_info  drop foreign key fk_user_id


    
alter table like_info  drop foreign key fk_like_category_id


    
alter table marital_status_info  drop foreign key fk_admin_id


    
alter table menu_info  drop foreign key fk_parent_id


    
alter table menu_info  drop foreign key fk_admin_id


    
alter table menu_info  drop foreign key fk_icon_id


    
alter table menu_info  drop foreign key fk_file_category_id


    
alter table skill_category_info  drop foreign key fk_admin_id


    
alter table skill_category_info  drop foreign key fk_parent_id


    
alter table skill_info  drop foreign key fk_user_id


    
alter table skill_info  drop foreign key fk_skill_category_id


    
alter table user_file_info  drop foreign key fk_file_category_id


    
alter table user_file_info  drop foreign key fk_admin_id


    
alter table user_file_info  drop foreign key fk_parent_id


    
alter table user_file_info  drop foreign key fk_user_id


    
alter table user_info  drop foreign key fk_head_pic_id


    
alter table user_info  drop foreign key fk_edution_category_id


    
alter table user_info  drop foreign key fk_marital_status_id


    
alter table user_info  drop foreign key fk_user_status_id


    
alter table user_info  drop foreign key fk_user_level_id


    
alter table user_info  drop foreign key fk_card_photo1_id


    
alter table user_info  drop foreign key fk_card_photo2_id


    
alter table user_info  drop foreign key fk_hand_card_photo1_id_id


    
alter table user_info  drop foreign key fk_hand_card_photo2_id


    
alter table user_info  drop foreign key fk_user_like_id


    
alter table user_level_info  drop foreign key fk_admin_id


    
alter table user_menu_info  drop foreign key fk_admin_id


    
alter table user_menu_info  drop foreign key fk_menu_id


    
alter table user_menu_info  drop foreign key fk_role_id


    
alter table user_menu_info  drop foreign key fk_level_id


    
alter table user_status_info  drop foreign key fk_admin_id


    
alter table user_tag_category_info  drop foreign key fk_parent_id


    
alter table user_tag_category_info  drop foreign key fk_admin_id


    
alter table user_tag_info  drop foreign key fk_user_id


    
alter table user_tag_info  drop foreign key fk_user_tag_category_id


    
alter table work_info  drop foreign key fk_user_id


    
alter table work_info  drop foreign key fk_job_category_id


    drop table if exists admin_info

    drop table if exists admin_role_info

    drop table if exists edution_category_info

    drop table if exists edution_info

    drop table if exists file_category_info

    drop table if exists icon_info

    drop table if exists job_category_info

    drop table if exists like_category_info

    drop table if exists like_info

    drop table if exists marital_status_info

    drop table if exists menu_info

    drop table if exists skill_category_info

    drop table if exists skill_info

    drop table if exists user_file_info

    drop table if exists user_info

    drop table if exists user_level_info

    drop table if exists user_menu_info

    drop table if exists user_status_info

    drop table if exists user_tag_category_info

    drop table if exists user_tag_info

    drop table if exists work_info

    create table admin_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       account VARCHAR(15) not null,
       password VARCHAR(50) not null,
       nick_name VARCHAR(10),
       login_date DATETIME,
       token VARCHAR(50),
       express_in INTEGER,
       real_name VARCHAR(10),
       birthday DATETIME,
       phone VARCHAR(11),
       sex VARCHAR(1),
       description TEXT,
       email VARCHAR(20),
       admin_role_id INTEGER,
       file_id INTEGER,
       login_ip VARCHAR(20),
       parent_id INTEGER,
       primary key (id)
    )

    create table admin_role_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       parent_id INTEGER,
       admin_id INTEGER,
       primary key (id)
    )

    create table edution_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table edution_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       edution_category_id INTEGER,
       primary key (id)
    )

    create table file_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       accept VARCHAR(50),
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table icon_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       name VARCHAR(20),
       style TEXT,
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table job_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       parent_id INTEGER,
       primary key (id)
    )

    create table like_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       parent_id INTEGER,
       primary key (id)
    )

    create table like_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       like_category_id INTEGER,
       primary key (id)
    )

    create table marital_status_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       name VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table menu_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       menu_name VARCHAR(20),
       menu_group VARCHAR(20),
       href VARCHAR(50),
       order_no INTEGER,
       description TEXT,
       parent_id INTEGER,
       admin_id INTEGER,
       icon_id INTEGER,
       collpse TINYINT(1),
       file_category_id INTEGER,
       primary key (id)
    )

    create table skill_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       parent_id INTEGER,
       primary key (id)
    )

    create table skill_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       skill_category_id INTEGER,
       primary key (id)
    )

    create table user_file_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       src VARCHAR(100),
       file_id VARCHAR(100),
       base64 MEDIUMTEXT,
       description TEXT,
       file_category_id INTEGER,
       admin_id INTEGER,
       parent_id INTEGER,
       user_id INTEGER,
       type VARCHAR(50),
       primary key (id)
    )

    create table user_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       account VARCHAR(15),
       password VARCHAR(20),
       real_name VARCHAR(20),
       nick_name VARCHAR(20),
       head_pic_id INTEGER,
       card_id VARCHAR(18),
       Bank_id VARCHAR(20),
       birthday DATETIME,
       phone VARCHAR(11),
       sex VARCHAR(2),
       edution_category_id INTEGER,
       marital_status_id INTEGER,
       description TEXT,
       email VARCHAR(30),
       user_status_id INTEGER,
       fail_count INTEGER,
       login_ip VARCHAR(12),
       user_level_id INTEGER,
       login_date DATETIME,
       card_photo1 INTEGER,
       card_photo2 INTEGER,
       hand_card_photo1_id INTEGER,
       hand_card_photo2_id INTEGER,
       card_photo_status TINYINT(1),
       user_like_id INTEGER,
       primary key (id)
    )

    create table user_level_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       name VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table user_menu_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       admin_id INTEGER,
       menu_id INTEGER,
       role_id INTEGER,
       level_id INTEGER,
       enable TINYINT(1),
       add1 TINYINT(1),
       modify1 TINYINT(1),
       delete1 TINYINT(1),
       query TINYINT(1),
       primary key (id)
    )

    create table user_status_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       name VARCHAR(20),
       description TEXT,
       admin_id INTEGER,
       primary key (id)
    )

    create table user_tag_category_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       category VARCHAR(20),
       description TEXT,
       parent_id INTEGER,
       admin_id INTEGER,
       primary key (id)
    )

    create table user_tag_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       user_id INTEGER,
       user_tag_category_id INTEGER,
       primary key (id)
    )

    create table work_info (
        id INTEGER not null,
       create_date DATETIME,
       update_date DATETIME,
       company_name VARCHAR(50),
       job VARCHAR(50),
       description TEXT,
       start_date DATETIME,
       end_date DATETIME,
       user_id INTEGER,
       job_category_id INTEGER,
       primary key (id)
    )

    alter table admin_info 
        add index (admin_role_id), 
        add constraint fk_admin_role_id 
        foreign key (admin_role_id) 
        references admin_role_info (id)

    alter table admin_info 
        add index (file_id), 
        add constraint fk_file_id 
        foreign key (file_id) 
        references user_file_info (id)

    alter table admin_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references admin_info (id)

    alter table admin_role_info 
        add index (parent_id), 
        add constraint fk_admin_role_id 
        foreign key (parent_id) 
        references admin_role_info (id)

    alter table admin_role_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table edution_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table edution_info 
        add index (user_id), 
        add constraint fk_admin_id 
        foreign key (user_id) 
        references user_info (id)

    alter table edution_info 
        add index (edution_category_id), 
        add constraint fk_edution_category_id 
        foreign key (edution_category_id) 
        references edution_category_info (id)

    alter table file_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table icon_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table job_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table job_category_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references job_category_info (id)

    alter table like_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table like_category_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references like_category_info (id)

    alter table like_info 
        add index (user_id), 
        add constraint fk_user_id 
        foreign key (user_id) 
        references user_info (id)

    alter table like_info 
        add index (like_category_id), 
        add constraint fk_like_category_id 
        foreign key (like_category_id) 
        references like_category_info (id)

    alter table marital_status_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table menu_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references menu_info (id)

    alter table menu_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table menu_info 
        add index (icon_id), 
        add constraint fk_icon_id 
        foreign key (icon_id) 
        references icon_info (id)

    alter table menu_info 
        add index (file_category_id), 
        add constraint fk_file_category_id 
        foreign key (file_category_id) 
        references file_category_info (id)

    alter table skill_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table skill_category_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references skill_category_info (id)

    alter table skill_info 
        add index (user_id), 
        add constraint fk_user_id 
        foreign key (user_id) 
        references user_info (id)

    alter table skill_info 
        add index (skill_category_id), 
        add constraint fk_skill_category_id 
        foreign key (skill_category_id) 
        references skill_category_info (id)

    alter table user_file_info 
        add index (file_category_id), 
        add constraint fk_file_category_id 
        foreign key (file_category_id) 
        references file_category_info (id)

    alter table user_file_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table user_file_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references user_file_info (id)

    alter table user_file_info 
        add index (user_id), 
        add constraint fk_user_id 
        foreign key (user_id) 
        references user_info (id)

    alter table user_info 
        add index (head_pic_id), 
        add constraint fk_head_pic_id 
        foreign key (head_pic_id) 
        references user_file_info (id)

    alter table user_info 
        add index (edution_category_id), 
        add constraint fk_edution_category_id 
        foreign key (edution_category_id) 
        references edution_category_info (id)

    alter table user_info 
        add index (marital_status_id), 
        add constraint fk_marital_status_id 
        foreign key (marital_status_id) 
        references marital_status_info (id)

    alter table user_info 
        add index (user_status_id), 
        add constraint fk_user_status_id 
        foreign key (user_status_id) 
        references user_status_info (id)

    alter table user_info 
        add index (user_level_id), 
        add constraint fk_user_level_id 
        foreign key (user_level_id) 
        references user_level_info (id)

    alter table user_info 
        add index (card_photo1), 
        add constraint fk_card_photo1_id 
        foreign key (card_photo1) 
        references user_file_info (id)

    alter table user_info 
        add index (card_photo2), 
        add constraint fk_card_photo2_id 
        foreign key (card_photo2) 
        references user_file_info (id)

    alter table user_info 
        add index (hand_card_photo1_id), 
        add constraint fk_hand_card_photo1_id_id 
        foreign key (hand_card_photo1_id) 
        references user_file_info (id)

    alter table user_info 
        add index (hand_card_photo2_id), 
        add constraint fk_hand_card_photo2_id 
        foreign key (hand_card_photo2_id) 
        references user_file_info (id)

    alter table user_info 
        add index (user_like_id), 
        add constraint fk_user_like_id 
        foreign key (user_like_id) 
        references like_category_info (id)

    alter table user_level_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table user_menu_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table user_menu_info 
        add index (menu_id), 
        add constraint fk_menu_id 
        foreign key (menu_id) 
        references menu_info (id)

    alter table user_menu_info 
        add index (role_id), 
        add constraint fk_role_id 
        foreign key (role_id) 
        references admin_role_info (id)

    alter table user_menu_info 
        add index (level_id), 
        add constraint fk_level_id 
        foreign key (level_id) 
        references user_level_info (id)

    alter table user_status_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table user_tag_category_info 
        add index (parent_id), 
        add constraint fk_parent_id 
        foreign key (parent_id) 
        references user_tag_category_info (id)

    alter table user_tag_category_info 
        add index (admin_id), 
        add constraint fk_admin_id 
        foreign key (admin_id) 
        references admin_info (id)

    alter table user_tag_info 
        add index (user_id), 
        add constraint fk_user_id 
        foreign key (user_id) 
        references user_info (id)

    alter table user_tag_info 
        add index (user_tag_category_id), 
        add constraint fk_user_tag_category_id 
        foreign key (user_tag_category_id) 
        references user_tag_category_info (id)

    alter table work_info 
        add index (user_id), 
        add constraint fk_user_id 
        foreign key (user_id) 
        references user_info (id)

    alter table work_info 
        add index (job_category_id), 
        add constraint fk_job_category_id 
        foreign key (job_category_id) 
        references job_category_info (id)
