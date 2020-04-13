/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 50528
 Source Host           : localhost:3306
 Source Schema         : company

 Target Server Type    : MySQL
 Target Server Version : 50528
 File Encoding         : 65001

 Date: 12/02/2020 20:09:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for about_info
-- ----------------------------
DROP TABLE IF EXISTS `about_info`;
CREATE TABLE `about_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `background_image` int(11) NULL DEFAULT NULL,
  `image` int(11) NULL DEFAULT NULL,
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_about_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_about_info_image_info_background_image`(`background_image`) USING BTREE,
  INDEX `FK_about_info_image_info_image`(`image`) USING BTREE,
  CONSTRAINT `FK_about_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_about_info_image_info_background_image` FOREIGN KEY (`background_image`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_about_info_image_info_image` FOREIGN KEY (`image`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of about_info
-- ----------------------------
INSERT INTO `about_info` VALUES (1, '2020-02-08 18:12:04', '2020-02-08 18:28:37', 1, '关于我们', 'About us', 'Photographs are a way of preserving a moment in our lives for the rest of our lives. Many of us have at least been tempted at the flashy array of photo printers which seemingly leap off the shelves at even the least tech-savvy. It surely seems old fashioned to talk about 35mm film and non-digital cameras in today’s day and age.', 'Photographs are a way of preserving a moment in our lives for the rest of our lives. Many of us have at least been tempted at the flashy array of photo printers which seemingly leap off the shelves at even the least tech-savvy. It surely seems old fashioned to talk about 35mm film and non-digital cameras in today’s day and age.', 48, 47, '我们是谁', 'Who we are', NULL);
INSERT INTO `about_info` VALUES (2, '2020-02-08 18:15:23', '2020-02-08 18:21:35', 0, 'test', 'test', NULL, NULL, 51, 52, NULL, NULL, NULL);
INSERT INTO `about_info` VALUES (3, '2020-02-08 18:22:22', '2020-02-08 18:23:47', 0, 'test2', 'test2', NULL, NULL, 53, 54, NULL, NULL, NULL);
INSERT INTO `about_info` VALUES (4, '2020-02-08 18:31:18', '2020-02-08 18:34:04', 0, 'test3', 'test3', NULL, NULL, 55, 56, NULL, NULL, NULL);
INSERT INTO `about_info` VALUES (5, '2020-02-08 18:34:27', '2020-02-08 19:19:32', 0, 'test4', 'test4', NULL, NULL, 63, 61, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for admin_info
-- ----------------------------
DROP TABLE IF EXISTS `admin_info`;
CREATE TABLE `admin_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `account` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `phone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `email` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `role_id` int(11) NULL DEFAULT NULL,
  `remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `english_remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sex` tinyint(255) NULL DEFAULT NULL,
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_admin_info_role_id`(`role_id`) USING BTREE,
  CONSTRAINT `FK_admin_info_admin_role_info_role_id` FOREIGN KEY (`role_id`) REFERENCES `admin_role_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of admin_info
-- ----------------------------
INSERT INTO `admin_info` VALUES (1, '2020-01-27 22:04:44', NULL, 1, 'admin', '12345678901', '123@qq.com', 1, NULL, NULL, 1, 'admin');

-- ----------------------------
-- Table structure for admin_role_info
-- ----------------------------
DROP TABLE IF EXISTS `admin_role_info`;
CREATE TABLE `admin_role_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_admin_role_info_admin_id`(`admin_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of admin_role_info
-- ----------------------------
INSERT INTO `admin_role_info` VALUES (1, '2020-01-27 15:57:07', NULL, '管理员', 'Manger', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for basic_category_info
-- ----------------------------
DROP TABLE IF EXISTS `basic_category_info`;
CREATE TABLE `basic_category_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `basic_category_info_ibfk_1` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of basic_category_info
-- ----------------------------
INSERT INTO `basic_category_info` VALUES (1, '2020-02-03 18:12:18', '2020-02-03 21:52:28', 1, '服务', 'Service', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (2, '2020-02-03 18:49:53', '2020-02-03 21:52:21', 1, '技能', 'Skill', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (3, '2020-02-03 21:50:49', '2020-02-03 21:52:13', 1, '感言', 'Testimonials ', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (4, '2020-02-03 23:25:21', NULL, 1, '主题', 'Theme', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (5, '2020-02-03 23:41:50', NULL, 1, '菜单分类', 'Menu Category', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (6, '2020-02-04 12:12:31', NULL, 1, '品牌', 'Brand', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (7, '2020-02-04 18:17:25', NULL, 1, '作品', 'Work', NULL, NULL, NULL);
INSERT INTO `basic_category_info` VALUES (8, '2020-02-09 14:41:56', NULL, 1, '团队', 'Team', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for brand_info
-- ----------------------------
DROP TABLE IF EXISTS `brand_info`;
CREATE TABLE `brand_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `logo` int(11) NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `feature` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_brand_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `IX_brand_info_category_id`(`category_id`) USING BTREE,
  INDEX `logo`(`logo`) USING BTREE,
  CONSTRAINT `brand_info_ibfk_1` FOREIGN KEY (`logo`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_brand_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_brand_info_category_info_category_id` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of brand_info
-- ----------------------------
INSERT INTO `brand_info` VALUES (1, '2020-02-04 13:23:36', '2020-02-04 14:45:11', 1, 24, 12, NULL, '#', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `brand_info` VALUES (2, '2020-02-04 13:24:41', '2020-02-04 14:45:28', 1, 25, 12, NULL, '#', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `brand_info` VALUES (3, '2020-02-04 13:24:46', '2020-02-04 14:45:45', 1, 26, 12, NULL, '#', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `brand_info` VALUES (4, '2020-02-04 13:24:50', '2020-02-04 14:46:37', 1, 27, 12, NULL, '#', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `brand_info` VALUES (5, '2020-02-04 13:24:54', '2020-02-04 14:47:11', 1, 28, 12, NULL, '#', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `brand_info` VALUES (6, '2020-02-05 13:24:54', NULL, 1, NULL, 14, NULL, '#', 'Fast', 'Fast', 'Having a baby can be a nerve wracking experience for new', 'Having a baby can be a nerve wracking experience for new', 'fa-rocket');
INSERT INTO `brand_info` VALUES (7, '2020-02-05 13:41:58', NULL, 1, NULL, 14, NULL, '#', 'Easy', 'Easy', 'If you are looking for a new way to promote your business that', 'If you are looking for a new way to promote your business that', 'fa-check');
INSERT INTO `brand_info` VALUES (8, '2020-02-05 13:45:20', '2020-02-05 13:46:33', 1, NULL, 14, NULL, '#', 'Cheap', 'Cheap', 'Okay, you\' ve decided you want to make money with Affiliate', 'Okay, you\' ve decided you want to make money with Affiliate', 'fa-bullhorn');
INSERT INTO `brand_info` VALUES (9, '2020-02-05 13:47:10', NULL, 1, NULL, 14, NULL, '#', 'Editable', 'Editable', 'A Pocket PC is a handheld computer, which features many', 'A Pocket PC is a handheld computer, which features many', 'fa-arrows');

-- ----------------------------
-- Table structure for category_info
-- ----------------------------
DROP TABLE IF EXISTS `category_info`;
CREATE TABLE `category_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `background_image` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_category_info_basic_category_info_category_id`(`category_id`) USING BTREE,
  INDEX `admin_id`(`admin_id`) USING BTREE,
  INDEX `background_image`(`background_image`) USING BTREE,
  CONSTRAINT `category_info_ibfk_1` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `category_info_ibfk_2` FOREIGN KEY (`background_image`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_category_info_basic_category_info_category_id` FOREIGN KEY (`category_id`) REFERENCES `basic_category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of category_info
-- ----------------------------
INSERT INTO `category_info` VALUES (1, '2020-02-03 18:19:17', '2020-02-04 18:23:58', 1, '我们的服务', 'Our Service', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 1, NULL, NULL);
INSERT INTO `category_info` VALUES (2, '2020-02-03 18:52:28', '2020-02-04 18:01:14', 1, '我们的技能', 'Our Skills', 'MySpace上的所有用户都将知道那里有数百万人。每天都有许多人加入这个社区。', 'All users on MySpace will know that there are millions of people out there. Every\nday besides so many people joining this community.', 2, NULL, 30);
INSERT INTO `category_info` VALUES (3, '2020-02-03 21:53:05', NULL, 1, '感言', 'Testimonials', '奥勒姆·伊普苏姆·多洛尔坐在阿梅特的位子上，自我陶醉，自我陶醉。小威尼斯', 'Lorem ipsum dolor sit amet, consectetur adipisicingelit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 3, NULL, NULL);
INSERT INTO `category_info` VALUES (4, '2020-02-03 23:25:52', NULL, 1, '公司', 'COMPANY', NULL, NULL, 4, NULL, NULL);
INSERT INTO `category_info` VALUES (5, '2020-02-03 23:26:07', NULL, 1, '支持', 'SUPPORT', NULL, NULL, 4, NULL, NULL);
INSERT INTO `category_info` VALUES (6, '2020-02-03 23:26:25', NULL, 1, '开发者', 'DEVELOPERS', NULL, NULL, 4, NULL, NULL);
INSERT INTO `category_info` VALUES (7, '2020-02-03 23:26:40', NULL, 1, '我们的合作伙伴', 'OUR PARTNERS', NULL, NULL, 4, NULL, NULL);
INSERT INTO `category_info` VALUES (8, '2020-02-03 23:46:14', NULL, 1, '平台', 'Platform', NULL, NULL, 5, NULL, NULL);
INSERT INTO `category_info` VALUES (9, '2020-02-03 23:46:33', NULL, 1, '商城', 'Shopping', NULL, NULL, 5, NULL, NULL);
INSERT INTO `category_info` VALUES (10, '2020-02-03 23:47:01', NULL, 1, '财务', 'Finance', NULL, NULL, 5, NULL, NULL);
INSERT INTO `category_info` VALUES (11, '2020-02-03 23:47:27', NULL, 1, '手机', 'Phone', NULL, NULL, 5, NULL, NULL);
INSERT INTO `category_info` VALUES (12, '2020-02-04 12:13:07', '2020-02-04 17:47:03', 1, '我们的合作伙伴', 'OUR PARTNERS', '奥勒姆·伊普苏姆·多洛尔坐在阿梅特的位子上，自我陶醉，自我陶醉。小威尼斯', 'Lorem ipsum dolor sit amet, consectetur adipisicingelit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 6, NULL, 29);
INSERT INTO `category_info` VALUES (13, '2020-02-04 18:19:39', NULL, 1, '最近作品', 'Recent Works', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt <br/>utet dolore magna aliqua. Ut enim ad minim veniam', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt <br/>utet dolore magna aliqua. Ut enim ad minim veniam', 7, NULL, NULL);
INSERT INTO `category_info` VALUES (14, '2020-02-05 13:06:14', '2020-02-05 13:11:35', 1, '特征', 'Features', 'Lorem ipsum dolor sit amet，consectetur adipiscing elite，sed do eiusmod tempor incidedunt utet dolore <br/>magna aliqua.我是一位杰出的建筑师。小威尼斯', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt <br/>utet dolore magna aliqua. Ut enim ad minim veniam', 6, NULL, NULL);
INSERT INTO `category_info` VALUES (15, '2020-02-09 14:41:39', '2020-02-09 14:42:08', 1, '我们的团队', 'Our Team', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utet dolore magna aliqua. Ut enim ad minim veniam', 8, NULL, NULL);
INSERT INTO `category_info` VALUES (16, '2020-02-09 16:48:48', NULL, 1, 'Jeffery', 'Jeffery Poole', NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for company_info
-- ----------------------------
DROP TABLE IF EXISTS `company_info`;
CREATE TABLE `company_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `tel` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `logo` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `logo1` int(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_company_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_company_info_image_info_logo`(`logo`) USING BTREE,
  INDEX `logo1`(`logo1`) USING BTREE,
  CONSTRAINT `company_info_ibfk_1` FOREIGN KEY (`logo1`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_company_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_company_info_image_info_logo` FOREIGN KEY (`logo`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of company_info
-- ----------------------------
INSERT INTO `company_info` VALUES (1, '2020-02-07 14:04:23', '2020-02-08 15:43:55', 1, '公司名称', 'Company Name', '.........', '.........', NULL, 45, NULL, NULL);

-- ----------------------------
-- Table structure for image_info
-- ----------------------------
DROP TABLE IF EXISTS `image_info`;
CREATE TABLE `image_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `src` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `type` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_image_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `FK_image_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 72 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of image_info
-- ----------------------------
INSERT INTO `image_info` VALUES (1, '2020-02-03 15:13:53', NULL, NULL, '19c78ced95964fd9a9b581d4c0dd1470', '7ccb11daa59e47b8aa5e0df131969ff6.svg', '1e64f42f053f4cc186d49fc2da595ad5.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (2, '2020-02-03 15:56:53', NULL, NULL, '5883e08a9524460598a11f6095eb193e', '6e833c6fdaed4ec4bd0d002f49ef16ae.svg', '73ebef95e9014ec48c0d7a09241ed7d0.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (6, '2020-02-03 16:02:38', NULL, NULL, '48b4b8352bec419fa676ef32b9a50bfc', '96fd6cad111a4a338a1300cbeb68c1cd.svg', '1266a2175a5e48ecaf9ea9f7ec54555f.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (7, '2020-02-03 16:03:14', NULL, NULL, '4741e4c47ef642feabe8c05f5ab90457', '53a45f9473fa4de1a2c7faa5b915eaec.svg', '9e296ba59fde42859012970e0e0b4718.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (8, '2020-02-03 16:03:39', NULL, NULL, '14a98fbf43064cf5a8ed0fc035268a21', 'fbdd5ad1aa53481ba334d7eca1592cf4.svg', 'e6e2202762904ade8d18684d0d69ed3d.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (9, '2020-02-03 16:04:34', NULL, NULL, '2e9a42c903624600829117dbab6f037a', 'e57b56f3149a4d439f1230d76e27926d.svg', '60285eaf78fc4533a43c39425c2a2e30.svg', NULL, 'service');
INSERT INTO `image_info` VALUES (10, '2020-02-03 23:10:07', NULL, NULL, 'd63a8b0bacc24acb9723600beab93e11', '8419afda96524798939bb56a93541c48.jpg', '7c2664abbe074c508067a10b0567a694.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (11, '2020-02-03 23:11:48', NULL, NULL, 'f8a3532f2485480091e6c2fdb421c3c1', '6c3ffcd01d864e3390162e844311b72a.jpg', 'a162d172e82a4a299df94565e9351a01.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (12, '2020-02-03 23:13:43', NULL, NULL, 'f548cfe249ca493dbc9c2752e70c699a', 'cefc741202f74979aa1cd1c1e8398aae.jpg', '3de341e5e78a4c349491581182e86afa.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (13, '2020-02-03 23:17:24', NULL, NULL, '59f3cd0a922d46a393f1df4bbf4f732c', '5b96ba1c6eb146c69cf97e201597be44.jpg', '0cf0eccc9f0d4e318933ac5a42202ed4.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (14, '2020-02-03 23:17:40', NULL, NULL, 'bf983c17f18148839cc64be75d4fd3d9', 'e60b2555b06b430fb116c7e77ee9c37b.jpg', '9ff2b257491c4a1785ce29d031809aa3.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (15, '2020-02-03 23:17:52', NULL, NULL, '469d93dfd4fa4f0197fef2be8b589282', '30a3d3a823184f39a5c2edb49208fff6.jpg', '08df6bd471cb4fd7bbd813bfc06edaac.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (16, '2020-02-03 23:18:13', NULL, NULL, 'ab345bcf99fe45a38e78114848cdb3b5', '820b4978f3044abf96829fb535f992ae.jpg', 'ab5b65b01fb44095b4bf7ccbade2edb1.jpg', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (17, '2020-02-03 23:18:21', NULL, NULL, '6ad9598ce4ef4eb88bae44ec26c1b608', '63c52bb664d6467eaca8e9511a2dfe04.png', '59d2a193407f4f6b9bc9cbae4e699a12.png', NULL, 'testimonial');
INSERT INTO `image_info` VALUES (18, '2020-02-04 13:23:36', NULL, NULL, '024d602b706c4890a84bcc346cb0fbcb', '7eb58e08a1bb4bd8bd5d2933d1acad50.png', '37401baf94dd47bd97fa0798271c8d00.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (19, '2020-02-04 13:24:41', NULL, NULL, '91f6d9076dc04171b23bc4ff045af29b', 'a0d9537342084901b2b63f16d33c1aad.png', '8581ea3c3fe44172b18108f8530209f8.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (20, '2020-02-04 13:24:46', NULL, NULL, 'a2cc92aba59e42149e7b0edee468386d', '7b526e87cb9a4ccc83f8de014511da8d.png', '3a5c1aa183694e179757ad6df14ccd12.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (21, '2020-02-04 13:24:50', NULL, NULL, '29a0244595934817b688595d37fe48f6', 'a10f62c9886e45ab9fb1743a93875602.png', '14159154875e4b11a0dbc38424365950.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (22, '2020-02-04 13:24:54', NULL, NULL, '2a3128ada71a46678264d0eb9f25cad6', 'f36d34fcfba14fcf98543301cb355994.png', 'c8f096f3712b4976a08bc821e419469b.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (24, '2020-02-04 14:45:11', NULL, NULL, '462e3a2d484e4f0eb8509e6ddd02330a', 'bd56ce44b74f4543a32a06643ea24e07.png', 'db397d717d5e4e41ab126521af555d61.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (25, '2020-02-04 14:45:28', NULL, NULL, '47914300e14540bb923d703272217255', '9a6d437a81004d3980427ef7b184a096.png', '63b066ed41fd4cb1a1233d280f3cb3ce.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (26, '2020-02-04 14:45:45', NULL, NULL, 'cd919b546a074563bca561f826436d01', '5da0755d2e7147d49213d95bde669f2a.png', '778e9dd485214b66b6479b01b1e29d7f.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (27, '2020-02-04 14:46:37', NULL, NULL, '011ea1f50b464bc38a6f75ec43802038', '4a9b90732d1e448aa0d8bc54f1ef9a70.png', '2ea4a0cf5f0f4ab99a7dbd699e7abf2f.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (28, '2020-02-04 14:47:11', NULL, NULL, '6cb06f52e7eb42818cddca4a77d52ff1', 'cf53688909fe433ead7fd8658cbbc93b.png', 'cd92d89b713244d08ba2d69413496fff.png', NULL, 'brand');
INSERT INTO `image_info` VALUES (29, '2020-02-04 17:47:03', NULL, NULL, '74e0f6e644244a11889d825d7786a3c7', '16f3f3c3a52e4061847feba4ab44e69f.png', 'b3dbd7c935e64f0baf8092f1bc3503c9.png', NULL, 'bg');
INSERT INTO `image_info` VALUES (30, '2020-02-04 18:01:13', NULL, NULL, 'd86f857ca63c48479b80b3121b13a2dc', 'b45cf750dd0d41cdb1d4e0ac4f92d7df.png', 'd99a8b7264b04e5baafd080aba4cd2dd.png', NULL, 'bg');
INSERT INTO `image_info` VALUES (31, '2020-02-05 11:31:23', NULL, NULL, '0ac45a39d5904422b0c33c9a69a761ce', '535de21eb6e04571ac9deaf8b1342f30.png', '538ac5401e4044aca13c7e29acbf7934.png', NULL, 'work');
INSERT INTO `image_info` VALUES (33, '2020-02-05 11:32:16', NULL, NULL, '0b52161659664e7194933ed14afefc8d', '7dade8fac56f4384b6fade87aaf7c16a.png', 'ede64f292d404028994f93663b67ae8a.png', NULL, 'work');
INSERT INTO `image_info` VALUES (34, '2020-02-05 11:32:17', NULL, NULL, '17a29354dc3e465e840815035c37af35', '4cc1041580654d1ba74b283b4b9f377b.png', 'fd00a7a3740549ceae4080edebdef4ff.png', NULL, 'work');
INSERT INTO `image_info` VALUES (35, '2020-02-05 11:32:17', NULL, NULL, 'b7934eb0c41140149196db4a11f74a07', '3a382283aead41e79f5ec4d5d3a07228.png', '4acd28b42a0844c38129c900d34477f6.png', NULL, 'work');
INSERT INTO `image_info` VALUES (36, '2020-02-05 11:32:17', NULL, NULL, '98dd73af4f6e456daefaed55c2ada722', '993529bc003847708978e7456fe37879.png', 'da1a52888b474a0aa7a3b154eab652a8.png', NULL, 'work');
INSERT INTO `image_info` VALUES (37, '2020-02-05 11:32:17', NULL, NULL, 'd027a515e3d04d4a889a2aa56bbeaf5f', '370510eb817f46b88313922e58da4471.png', '8174621540864c358774f7ca7cf6a09d.png', NULL, 'work');
INSERT INTO `image_info` VALUES (38, '2020-02-05 11:32:17', NULL, NULL, 'eefa7e3c300246e1bae30c6447332372', '0f6facfdcdcd4cb6b7cd96dc0a908a2e.png', '03eac42b648d446ea4f517e8aef6a4a4.png', NULL, 'work');
INSERT INTO `image_info` VALUES (41, '2020-02-06 11:34:57', NULL, NULL, 'b22b2b1a307a4d65aebf65c35a553f72', '59f3cf45579a479ca285abea42957dfc.jpg', '5ee16cb7e6504e8391e384ca66ba4b4e.jpg', NULL, 'bg');
INSERT INTO `image_info` VALUES (42, '2020-02-06 11:43:29', NULL, NULL, '5a09aff395d0484da0c0f6b4fd076033', 'cdfb71e55140411b9b12b68fb8b0a563.jpg', '99a4a07a34764b31b679a6a224abce8c.jpg', NULL, 'bg');
INSERT INTO `image_info` VALUES (43, '2020-02-06 12:35:53', NULL, NULL, '5bcec39da9dc46d5a20eb7c28a2d35e2', 'f456f11fac8f4c829286942efc0107d6.png', 'a7a07030eefd495daa8256a86ebe1a77.png', NULL, 'bg');
INSERT INTO `image_info` VALUES (44, '2020-02-07 14:04:23', NULL, NULL, 'a5d7b1788c4a4db89c4dbedb5d2f97f5', '3c954c81398b417497a0cbbb3681eb83.jpg', '72d54b20ca664d5f8e6047c9707c320c.jpg', NULL, 'image');
INSERT INTO `image_info` VALUES (45, '2020-02-08 15:43:54', NULL, NULL, '33529a4c573d498fb574a71be4f2847d', 'b81dfa6d9ee44b9baf6541e580ead7f9.png', '0a4733d04cdd4dffa981e5e18259426b.png', NULL, 'image');
INSERT INTO `image_info` VALUES (46, '2020-02-08 18:12:04', NULL, NULL, '50456cf2b2554d1e93ad233f3a4d0f31', 'a2da65475a0b4a7c923534f76f25b7f0.png', 'd9e212d843ea4043b8172eae770461eb.png', NULL, 'image');
INSERT INTO `image_info` VALUES (47, '2020-02-08 18:14:21', NULL, NULL, '73963d35751243bfa265e05e4a2229a4', 'a675d1ec054b493fa7811e5a7f38043a.png', '8ef797a7e46d4e46958d9d3c026a3868.png', NULL, 'image');
INSERT INTO `image_info` VALUES (48, '2020-02-08 18:14:45', NULL, NULL, '9e4b3a1d644648c58ba9219254243e91', '3a555807da9f4b7baae91ea185d6c97b.png', '013db9b5208b466b833349122786e4a3.png', NULL, 'image');
INSERT INTO `image_info` VALUES (49, '2020-02-08 18:15:23', NULL, NULL, '2f805584ca9640c486821732f529a731', '8b5910613ce74ff38b2e41f7ad28cf73.png', '1700f58c75c346dbbfcac518b300ba44.png', NULL, 'image');
INSERT INTO `image_info` VALUES (50, '2020-02-08 18:17:37', NULL, NULL, '9590a1d79b07401e9d3d5473ca33277a', '676f1bf3946b4751a215971181560d73.png', '9262ee0bfdea4183a7112c304319e218.png', NULL, 'image');
INSERT INTO `image_info` VALUES (51, '2020-02-08 18:18:54', NULL, NULL, '8c71bde98d21493c95c256ae8524413f', '304e0377791e4b94be0d33c5d1aaacc9.png', 'c73ce2fee0444a6894994472c31afd03.png', NULL, 'image');
INSERT INTO `image_info` VALUES (52, '2020-02-08 18:21:35', NULL, NULL, 'd1a8378c69ac42a5997b87373f3a3e71', '76b97c42dbdb4775bd2e97de46082f52.png', 'd619536234ce4ab5b00da21f6bee5755.png', NULL, 'image');
INSERT INTO `image_info` VALUES (53, '2020-02-08 18:22:22', NULL, NULL, '2faf79aecb6d435d9be384af172f7555', '0bbc13885b9b44c5a33fe075e7297d62.png', '426c3112540a4e85803df8199d056744.png', NULL, 'image');
INSERT INTO `image_info` VALUES (54, '2020-02-08 18:23:47', NULL, NULL, 'c172178533f243bf8cfe394b308720e9', 'b5319e1a70f746d48d50c06cc4e08605.png', '6bb0fd619c184c48bc3bc9c8fb49e516.png', NULL, 'image');
INSERT INTO `image_info` VALUES (55, '2020-02-08 18:31:18', NULL, NULL, 'b81bc37200e1429fbf80f0bd25179a58', '528d9f41890949f1a99346ba0df3f04a.png', '924f6e6b8ebc4528951aa947fac63bdb.png', NULL, 'image');
INSERT INTO `image_info` VALUES (56, '2020-02-08 18:34:03', NULL, NULL, 'cb52962f3b644cae9284c15d77aab509', 'a263f5dee6b84e4c8c7c3274d4dc0bf5.jpg', 'bccebe2c870e4879b19f772e93b2c6bb.jpg', NULL, 'image');
INSERT INTO `image_info` VALUES (57, '2020-02-08 18:34:27', NULL, NULL, '9d959a98ed8a40b5a5f26a57647d6a03', 'a1b32aaa825a4dcda6d0eee2a86bdc65.png', '748e2919289a491b8b5afffe6076bd6d.png', NULL, 'image');
INSERT INTO `image_info` VALUES (58, '2020-02-08 18:34:27', NULL, NULL, 'd627130336374ce9b1a9c3e966c9119c', '732cb08a086d4fe898215691432f72c1.png', 'fba80d0971d44fd18e1aa929151bf972.png', NULL, 'image');
INSERT INTO `image_info` VALUES (59, '2020-02-08 18:34:51', NULL, NULL, 'cfe5b5fda09b4ca883eafd2f09bd2072', 'ef1de13d2d034ec49247f1ba1d833936.png', '4fc437cb284e4325bea104bdb86eea6c.png', NULL, 'image');
INSERT INTO `image_info` VALUES (60, '2020-02-08 18:36:23', NULL, NULL, '182671815157454794a2d6198474e8ab', '394d75519f0b4eba8e61a53a27bc86d7.png', '24198d91413a4181b833690d63a7545b.png', NULL, 'image');
INSERT INTO `image_info` VALUES (61, '2020-02-08 18:36:24', NULL, NULL, 'a557f9a215194d2c94c52df3eb7adf9b', '121a11a88f73438d85e3ddab5e598475.png', '19e066cebf8745c2aea4e7f786e7c423.png', NULL, 'image');
INSERT INTO `image_info` VALUES (62, '2020-02-08 18:38:33', NULL, NULL, '135e30da993447f99c0374282c36da72', '00102f64ac1a4af7a309f7ebdeb0deb3.png', '16067813d8344e698abf3c26c69b6223.png', NULL, 'image');
INSERT INTO `image_info` VALUES (63, '2020-02-08 18:40:03', NULL, NULL, '66b09edcfc824881a13b973c24b5980e', 'c32831ff5b5046b78344ef5845f2eda1.png', 'f072f07e9a474950a147897171316f75.png', NULL, 'image');
INSERT INTO `image_info` VALUES (64, '2020-02-09 16:50:32', NULL, NULL, '0dfedd87e238454383bac045a6039116', '00b501e08d374a2babc269dec28cb879.png', 'ed238dd215ff4d9bb15963362d5e7e32.png', NULL, 'team');
INSERT INTO `image_info` VALUES (65, '2020-02-09 16:52:56', NULL, NULL, 'd4967bffc80b4840bfe58131babd8e31', '5cbb3991f324465199fd80c70bc7ae3d.png', '12969950d1004f52b53f96f4a52082d8.png', NULL, 'team');
INSERT INTO `image_info` VALUES (66, '2020-02-09 16:54:38', NULL, NULL, 'b23c688ee2924c678faacddb3af674b7', 'ab5b7a8fdf844ac3af033bf184078dc1.png', 'a3ddf801c87b444bb0f500cce89c8d77.png', NULL, 'team');
INSERT INTO `image_info` VALUES (67, '2020-02-09 16:58:48', NULL, NULL, '89afadcf06344be9a95c3424e6d30090', '5397f4d3b6dd43c08b3075938dd91d9f.png', '2ee25e5efada4ee38d670e14292e40e0.png', NULL, 'team');
INSERT INTO `image_info` VALUES (68, '2020-02-09 16:59:50', NULL, 1, '5411b6ca001445d98086eb8666ab29b1', '986ac87b904c4ba2bafb7dff51e2de26.png', 'b8c3b2e8492d483ba1ec9a693ba8b388.png', NULL, 'team');
INSERT INTO `image_info` VALUES (69, '2020-02-09 17:01:56', NULL, 1, 'd0751b90cad9467fa78296eab8b546ff', '4e0f71b2d64340bd93e42428edf44691.png', 'bc0abd682df243aaa80e7e6cacb719c2.png', NULL, 'team');
INSERT INTO `image_info` VALUES (70, '2020-02-09 17:03:03', NULL, 1, '3a2dcabd45974f2f9a7ff2dcd3c92d21', '03d2c488c8794cb68d2624449689f53a.jpg', '06667e5ce7454bfcaa74009c9d05e792.jpg', NULL, 'team');
INSERT INTO `image_info` VALUES (71, '2020-02-09 17:05:01', NULL, 1, '99c8d931fdaf4907a4329d2020f97f4a', 'a959a177e4f2458e878b33a62f1b1bd3.jpg', '0e844f5bef174ef3aeab3cebaa83e9c2.jpg', NULL, 'team');

-- ----------------------------
-- Table structure for main_info
-- ----------------------------
DROP TABLE IF EXISTS `main_info`;
CREATE TABLE `main_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `background_image` int(11) NULL DEFAULT NULL,
  `button_href1` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `button_name1` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_button_name1` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `button_href2` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `button_name2` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_button_name2` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_main_info_image_info_background_image`(`background_image`) USING BTREE,
  INDEX `FK_main_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `FK_main_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_main_info_image_info_background_image` FOREIGN KEY (`background_image`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of main_info
-- ----------------------------
INSERT INTO `main_info` VALUES (1, '2020-02-06 11:34:57', '2020-02-06 12:09:50', 1, 'Help Finding Information Online', 'Help Finding Information Online', 'Every new computer that’s brought home from the store has an operating system installed onto it.', 'Every new computer that’s brought home from the store has an operating system installed onto it.', 41, '#', 'Learn More', 'Learn More', '#', 'Get Started', 'Get Started', NULL);
INSERT INTO `main_info` VALUES (2, '2020-02-06 11:43:29', '2020-02-06 12:09:59', 1, 'Help Finding Information Online', 'Help Finding Information Online', 'Every new computer that’s brought home from the store has an operating system installed onto it.', 'Every new computer that’s brought home from the store has an operating system installed onto it.', 42, '#', 'Learn More', 'Learn More', '#', 'Get Started', 'Get Started', NULL);
INSERT INTO `main_info` VALUES (3, '2020-02-06 12:35:53', NULL, 1, 'Help Finding Information Online', 'Help Finding Information Online', 'Every new computer that’s brought home from the store has an operating system installed onto it. ', 'Every new computer that’s brought home from the store has an operating system installed onto it. ', 43, '#', 'Learn More', 'Learn More', '#', 'Get Started', 'Get Started', NULL);

-- ----------------------------
-- Table structure for media_info
-- ----------------------------
DROP TABLE IF EXISTS `media_info`;
CREATE TABLE `media_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `body` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_body` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `src` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_media_info_admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `FK_media_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of media_info
-- ----------------------------
INSERT INTO `media_info` VALUES (1, '2020-01-28 14:24:00', NULL, 1, '响应式Web设计', 'Responsive Web Design', ' <div class=\"media\"> <div class=\"pull-left\"> <img class=\"img-responsive\" src=\"/company/images/tab2.png\"> </div> <div class=\"media-body\"> <h2>告别精英</h2><p>有许多不同的版本，但大多数都受到了某种形式的改变，通过注入幽默，或随机的文字，看起来甚至不太可信。如果你要用的话</p> </div> </div>', ' <div class=\"media\"> <div class=\"pull-left\"> <img class=\"img-responsive\" src=\"/company/images/tab2.png\"> </div> <div class=\"media-body\"> <h2>Adipisicing elit</h2> <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don\'t look even slightly believable. If you are going to use.</p> </div> </div>', NULL, NULL);
INSERT INTO `media_info` VALUES (2, '2020-01-28 14:27:15', NULL, 1, '包括高级插件', 'Premium Plugin Included', ' <div class=\"video-box\"> <img src=\"/company/images/tab-video-bg.png\" alt=\"video\"> <a class=\"video-icon\" href=\"http://www.youtube.com/watch?v=cH6kxtzovew\" rel=\"prettyPhoto\"><i class=\"fa fa-play\"></i></a> </div>', ' <div class=\"video-box\"> <img src=\"/company/images/tab-video-bg.png\" alt=\"video\"> <a class=\"video-icon\" href=\"http://www.youtube.com/watch?v=cH6kxtzovew\" rel=\"prettyPhoto\"><i class=\"fa fa-play\"></i></a> </div>', NULL, NULL);
INSERT INTO `media_info` VALUES (3, '2020-01-28 14:40:28', NULL, 1, '预定义布局', 'Predefine Layout', 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit.', '   <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit.</p>', NULL, NULL);
INSERT INTO `media_info` VALUES (4, '2020-01-28 14:42:55', NULL, 1, '我们的理念', 'Our Philosopy', '<p>Lorem Ipsum的段落有很多变化，但大多数都因注入幽默感或看起来有些难以置信的随机单词而以某种形式发生了变化。如果要使用Lorem Ipsum的段落，则需要确保文本中间没有隐藏任何令人尴尬的内容。Internet上的所有Lorem Ipsum生成器都倾向于根据需要重复预定义的块，这使其成为Internet上第一个真正的生成器。它使用超过200个拉丁词的字典</p>', ' <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don\'t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn\'t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words</p>', NULL, NULL);
INSERT INTO `media_info` VALUES (5, '2020-01-28 14:43:31', NULL, 1, '我们所做的？', 'What We Do?', '<p>Lorem Ipsum的段落有很多变体，但是大多数都因注入幽默感或看起来有些难以置信的随机单词而以某种形式发生了变化。如果要使用Lorem Ipsum的段落，则需要确保文本中间没有隐藏任何令人尴尬的内容。Internet上的所有Lorem Ipsum生成器都倾向于根据需要重复预定义的块，这使其成为Internet上第一个真正的生成器。它使用包含200多个拉丁词的字典，并结合了一些模型句子结构，</p>', ' <p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don\'t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn\'t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures,</p>', NULL, NULL);

-- ----------------------------
-- Table structure for menu_info
-- ----------------------------
DROP TABLE IF EXISTS `menu_info`;
CREATE TABLE `menu_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `icon` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `id_name` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `parent_id` int(11) NULL DEFAULT NULL,
  `enable` tinyint(1) NOT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_menu_info_parent_id`(`parent_id`) USING BTREE,
  INDEX `IX_menu_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `category_id`(`category_id`) USING BTREE,
  CONSTRAINT `FK_menu_info_menu_info_parent_id` FOREIGN KEY (`parent_id`) REFERENCES `menu_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `menu_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `menu_info_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 74 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of menu_info
-- ----------------------------
INSERT INTO `menu_info` VALUES (2, '2020-01-13 00:05:15', NULL, ' 管理员管理', NULL, '&#xe62d;', NULL, 'menu-admin', NULL, 1, NULL, 8);
INSERT INTO `menu_info` VALUES (3, '2020-01-13 00:05:15', NULL, ' 会员管理', NULL, '&#xe60d;', NULL, 'menu-member', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (4, '2020-01-13 00:05:15', NULL, ' 评论管理', NULL, '&#xe622;', NULL, 'menu-comments', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (5, '2020-01-13 00:05:15', NULL, ' 产品管理', NULL, '&#xe620;', NULL, 'menu-product', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (6, '2020-01-13 00:05:15', NULL, ' 图片管理', NULL, '&#xe613;', NULL, 'menu-picture', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (7, '2020-01-13 00:05:15', NULL, ' 资讯管理', NULL, '&#xe616;', NULL, 'menu-article', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (8, '2020-01-13 00:05:15', NULL, ' 系统管理', NULL, '&#xe62e;', NULL, 'menu-system', NULL, 0, NULL, 8);
INSERT INTO `menu_info` VALUES (9, '2020-01-13 00:05:15', NULL, ' 二级导航1', NULL, '&#xe616;', NULL, 'menu-aaaaa', NULL, 0, NULL, 9);
INSERT INTO `menu_info` VALUES (10, '2020-01-13 00:05:15', NULL, ' 二级导航2', NULL, '&#xe616;', NULL, 'menu-bbbbb', NULL, 0, NULL, 10);
INSERT INTO `menu_info` VALUES (11, '2020-01-13 00:05:15', NULL, ' 二级导航3', NULL, '&#xe616;', NULL, 'menu-ccccc', NULL, 0, NULL, 11);
INSERT INTO `menu_info` VALUES (13, '2020-01-13 00:05:15', NULL, '三级导航', NULL, NULL, 'article-list.html', NULL, 9, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (14, '2020-01-13 00:05:15', NULL, '系统日志', NULL, NULL, 'system-log.html', NULL, 8, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (15, '2020-01-13 00:05:15', NULL, '屏蔽词', NULL, NULL, 'system-shielding.html', NULL, 8, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (16, '2020-01-13 00:05:15', NULL, '数据字典', NULL, NULL, 'system-data.html', NULL, 8, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (17, '2020-01-13 00:05:15', NULL, '栏目管理', NULL, NULL, 'system-category.html', NULL, 8, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (18, '2020-01-13 00:05:15', NULL, '系统设置', NULL, NULL, 'system-base.html', NULL, 8, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (19, '2020-01-13 00:05:15', NULL, '资讯管理', NULL, NULL, 'article-list.html', NULL, 7, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (20, '2020-01-13 00:05:15', NULL, '图片管理', NULL, NULL, 'picture-list.html', NULL, 6, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (21, '2020-01-13 00:05:15', '2020-02-04 11:39:58', '品牌管理', 'Brand Manger', NULL, 'brand.html', NULL, 5, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (22, '2020-01-13 00:05:15', NULL, '分类管理', NULL, NULL, 'product-category.html', NULL, 5, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (23, '2020-01-13 00:05:15', NULL, '产品管理', NULL, NULL, 'product-list.html', NULL, 5, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (24, '2020-01-13 00:05:15', NULL, '评论列表', NULL, NULL, 'http://h-ui.duoshuo.com/admin/', NULL, 4, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (25, '2020-01-13 00:05:15', NULL, '意见反馈', NULL, NULL, 'feedback-list.html', NULL, 4, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (26, '2020-01-13 00:05:15', NULL, '会员列表', NULL, NULL, 'member-list.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (27, '2020-01-13 00:05:15', NULL, '删除的会员', NULL, NULL, 'member-del.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (28, '2020-01-13 00:05:15', NULL, '等级管理', NULL, NULL, 'member-level.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (29, '2020-01-13 00:05:15', NULL, '积分管理', NULL, NULL, 'member-scoreoperation.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (30, '2020-01-13 00:05:15', NULL, '浏览记录', NULL, NULL, 'member-record-browse.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (31, '2020-01-13 00:05:15', NULL, '下载记录', NULL, NULL, 'member-record-download.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (32, '2020-01-13 00:05:15', NULL, '分享记录', NULL, NULL, 'member-record-share.html', NULL, 3, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (33, '2020-01-13 00:05:15', NULL, '角色管理', NULL, NULL, 'admin-role.html', NULL, 2, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (34, '2020-01-13 00:05:15', NULL, '权限管理', NULL, NULL, 'admin-permission.html', NULL, 2, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (35, '2020-01-13 00:05:15', NULL, '管理员列表', NULL, NULL, 'admin-list.html', NULL, 2, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (42, '2020-01-13 00:05:15', NULL, '三级导航', NULL, NULL, 'article-list.html', NULL, 10, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (43, '2020-01-13 00:05:15', NULL, '三级导航', NULL, NULL, 'article-list.html', NULL, 11, 0, NULL, NULL);
INSERT INTO `menu_info` VALUES (44, '2020-01-24 00:25:45', NULL, '菜单系统', 'menu system', '', NULL, NULL, NULL, 1, NULL, 8);
INSERT INTO `menu_info` VALUES (48, '2020-01-24 00:25:56', NULL, '菜单管理', 'menu manger', NULL, 'menu.html', NULL, 44, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (49, '2020-01-24 00:26:08', NULL, '皮肤系统', 'skin manger', '', NULL, NULL, NULL, 1, NULL, 8);
INSERT INTO `menu_info` VALUES (50, '2020-01-24 00:26:14', NULL, '皮肤管理', 'skin system', NULL, 'skin.html', NULL, 49, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (51, '2020-01-24 01:01:37', '2020-02-03 18:49:09', '分类系统', 'Category System', ' ', NULL, NULL, NULL, 1, NULL, 8);
INSERT INTO `menu_info` VALUES (53, '2020-01-24 01:02:38', '2020-02-03 23:38:28', '主题管理', 'theme manger', NULL, 'theme.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (54, '2020-01-24 23:42:36', NULL, '官网系统', 'company system', '&#xe62e;', NULL, NULL, NULL, 1, NULL, 8);
INSERT INTO `menu_info` VALUES (56, '2020-01-25 17:15:34', NULL, '感言个人管理', 'Testimonaol Person Manger', NULL, 'testimonial_person.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (58, '2020-01-27 23:58:36', NULL, '媒体管理', 'Media Manger', NULL, 'media.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (60, '2020-01-28 16:53:35', NULL, '技能管理', 'Skill Manger', NULL, 'skill.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (62, '2020-01-28 18:49:52', NULL, '服务管理', 'Service Manger', NULL, 'service.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (63, '2020-02-03 18:08:53', NULL, '基本分类管理', 'Basic Category Manger', NULL, 'basic_category.html', NULL, 51, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (64, '2020-02-03 18:13:14', NULL, '分类管理', 'Category Manger', NULL, 'category.html', NULL, 51, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (65, '2020-02-04 18:29:10', '2020-02-10 21:07:43', '作品管理', 'Work Manger', NULL, 'work.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (66, '2020-02-06 11:19:48', NULL, '公司主页中间管理', 'Company Home Main Manger', NULL, 'main.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (67, '2020-02-06 14:31:51', NULL, '公司管理', 'Company Manger', NULL, 'company.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (68, '2020-02-08 12:17:27', NULL, '导航', 'Navs', NULL, 'nav.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (69, '2020-02-08 17:59:46', NULL, '关于我们管理', 'About Us Manger', NULL, 'about.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (70, '2020-02-09 14:34:56', NULL, '社交管理', 'Social Manger', NULL, 'social.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (71, '2020-02-09 16:27:03', NULL, '团队管理', 'Team Manger', NULL, 'team.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (72, '2020-02-10 12:24:13', NULL, '作品分类管理', 'Work Category Manger', NULL, 'work_category.html', NULL, 54, 1, NULL, NULL);
INSERT INTO `menu_info` VALUES (73, '2020-02-10 23:34:16', NULL, '图片展示', 'Image Show', NULL, 'picture-show.html', NULL, 6, 1, NULL, NULL);

-- ----------------------------
-- Table structure for nav_info
-- ----------------------------
DROP TABLE IF EXISTS `nav_info`;
CREATE TABLE `nav_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `parent_id` int(11) NULL DEFAULT NULL,
  `company_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_nav_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_nav_info_company_info_company_id`(`company_id`) USING BTREE,
  INDEX `FK_nav_info_nav_info_parent_id`(`parent_id`) USING BTREE,
  CONSTRAINT `FK_nav_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_nav_info_company_info_company_id` FOREIGN KEY (`company_id`) REFERENCES `company_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_nav_info_nav_info_parent_id` FOREIGN KEY (`parent_id`) REFERENCES `nav_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of nav_info
-- ----------------------------
INSERT INTO `nav_info` VALUES (1, '2020-02-08 12:54:31', '2020-02-10 21:17:12', 1, '首页', 'Index', 'index.html', 1, NULL, NULL);
INSERT INTO `nav_info` VALUES (2, '2020-02-08 13:07:25', '2020-02-08 14:14:39', 1, '关于我们', 'About Us', 'about-us.html', 2, NULL, NULL);
INSERT INTO `nav_info` VALUES (3, '2020-02-08 13:08:01', '2020-02-08 14:47:02', 1, '服务', 'Services', 'services.html', 3, NULL, NULL);
INSERT INTO `nav_info` VALUES (4, '2020-02-08 13:08:30', '2020-02-08 14:47:07', 1, '简介', 'Portfolio', 'portfolio.html', 4, NULL, NULL);
INSERT INTO `nav_info` VALUES (6, '2020-02-08 14:55:08', '2020-02-08 14:56:46', 1, '主页', 'Page', NULL, 6, NULL, NULL);
INSERT INTO `nav_info` VALUES (7, '2020-02-08 14:56:19', '2020-02-08 14:56:58', 1, '单博客', 'Single Blog', 'blog-item.html', 6, NULL, NULL);
INSERT INTO `nav_info` VALUES (8, '2020-02-08 14:57:51', NULL, 1, '价格', 'Pricing', 'pricing.html', 6, NULL, NULL);
INSERT INTO `nav_info` VALUES (9, '2020-02-08 14:59:34', '2020-02-08 14:59:44', 1, '博客', 'Blog', 'blog.html', 9, NULL, NULL);
INSERT INTO `nav_info` VALUES (10, '2020-02-08 15:00:20', '2020-02-08 15:00:31', 1, '联系我们', 'Contact Us', 'contact-us.html', 10, NULL, NULL);
INSERT INTO `nav_info` VALUES (11, '2020-02-08 16:14:15', NULL, 1, '404', '404', '404.html', 6, NULL, NULL);

-- ----------------------------
-- Table structure for service_info
-- ----------------------------
DROP TABLE IF EXISTS `service_info`;
CREATE TABLE `service_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `img` int(100) NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_service_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_service_info_admin_info_img_id`(`img`) USING BTREE,
  INDEX `service_info_category_info_category_id`(`category_id`) USING BTREE,
  CONSTRAINT `FK_service_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_service_info_admin_info_img_id` FOREIGN KEY (`img`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `service_info_category_info_category_id` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of service_info
-- ----------------------------
INSERT INTO `service_info` VALUES (1, '2020-01-29 16:34:31', '2020-02-03 15:56:31', 1, 'UI/UX 设计', 'UI/UX Design', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 1, 1, NULL);
INSERT INTO `service_info` VALUES (2, '2020-02-03 15:56:53', '2020-02-03 15:57:08', 1, 'Web 设计', 'Web Design', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 2, 1, NULL);
INSERT INTO `service_info` VALUES (3, '2020-02-03 16:02:38', NULL, 1, 'Motion 绘图', 'Motion Graphics', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 6, 1, NULL);
INSERT INTO `service_info` VALUES (4, '2020-02-03 16:03:14', NULL, 1, '移动端 UI/UX', 'Mobile UI/UX', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 7, 1, NULL);
INSERT INTO `service_info` VALUES (5, '2020-02-03 16:03:39', NULL, 1, 'Web 应用', 'Web Applications', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 8, 1, NULL);
INSERT INTO `service_info` VALUES (6, '2020-02-03 16:04:34', NULL, 1, 'SEO 制造', 'SEO Marketing', 'Hydroderm is the highly desired anti-aging cream on', 'Hydroderm is the highly desired anti-aging cream on', 9, 1, NULL);

-- ----------------------------
-- Table structure for skill_info
-- ----------------------------
DROP TABLE IF EXISTS `skill_info`;
CREATE TABLE `skill_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `process` int(11) NOT NULL,
  `style` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_skill_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_skill_info_category_info_CategoryId`(`category_id`) USING BTREE,
  CONSTRAINT `FK_skill_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_skill_info_category_info_CategoryId` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of skill_info
-- ----------------------------
INSERT INTO `skill_info` VALUES (1, '2020-01-28 17:27:55', NULL, NULL, '平面设计', 'Graphic Design', 85, '    background: #2d7da4;', 2, NULL);
INSERT INTO `skill_info` VALUES (2, '2020-01-28 17:29:07', NULL, 1, 'CSS', 'CSS', 80, '   background: #6aa42f;', 2, NULL);
INSERT INTO `skill_info` VALUES (3, '2020-01-28 17:29:32', NULL, 1, 'HTML', 'HTML', 95, '    background: #ffcc33;', 2, NULL);
INSERT INTO `skill_info` VALUES (4, '2020-01-28 17:57:44', NULL, 1, 'WordPress', 'WordPress', 90, 'background: #db3615;', 2, NULL);

-- ----------------------------
-- Table structure for skin_info
-- ----------------------------
DROP TABLE IF EXISTS `skin_info`;
CREATE TABLE `skin_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `enable` tinyint(1) NOT NULL,
  `color` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `skin_admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `skin_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of skin_info
-- ----------------------------
INSERT INTO `skin_info` VALUES (1, '2020-01-25 17:50:20', NULL, '默认（黑色）', 'Default (black)', 1, 'black', NULL);
INSERT INTO `skin_info` VALUES (2, '2020-01-13 00:05:15', NULL, '蓝色', NULL, 0, 'blue', NULL);
INSERT INTO `skin_info` VALUES (3, '2020-01-13 00:05:15', NULL, '绿色', NULL, 0, 'green', NULL);
INSERT INTO `skin_info` VALUES (4, '2020-01-13 00:05:15', NULL, '红色', NULL, 1, 'red', NULL);
INSERT INTO `skin_info` VALUES (5, '2020-01-13 00:05:15', NULL, '黄色', NULL, 0, 'yellow', NULL);
INSERT INTO `skin_info` VALUES (6, '2020-01-13 00:05:15', NULL, '橙色', NULL, 0, 'orange', NULL);

-- ----------------------------
-- Table structure for social_info
-- ----------------------------
DROP TABLE IF EXISTS `social_info`;
CREATE TABLE `social_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `company_id` int(11) NULL DEFAULT NULL,
  `parent_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_social_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_social_info_company_info_company_id`(`company_id`) USING BTREE,
  INDEX `FK_social_info_social_info_parent_id`(`parent_id`) USING BTREE,
  CONSTRAINT `FK_social_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_social_info_company_info_company_id` FOREIGN KEY (`company_id`) REFERENCES `company_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_social_info_social_info_parent_id` FOREIGN KEY (`parent_id`) REFERENCES `social_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of social_info
-- ----------------------------
INSERT INTO `social_info` VALUES (1, '2020-02-09 14:36:20', NULL, 1, 'fa-facebook', '#', NULL, NULL, NULL);
INSERT INTO `social_info` VALUES (2, '2020-02-09 14:36:31', NULL, 1, 'fa-twitter', '#', NULL, NULL, NULL);
INSERT INTO `social_info` VALUES (3, '2020-02-09 14:36:40', NULL, 1, 'fa-linkedin', '#', NULL, NULL, NULL);
INSERT INTO `social_info` VALUES (4, '2020-02-09 14:36:47', NULL, 1, 'fa-dribbble', '#', NULL, NULL, NULL);
INSERT INTO `social_info` VALUES (5, '2020-02-09 14:37:01', '2020-02-09 16:39:40', 1, 'fa-skype', '#', NULL, NULL, NULL);
INSERT INTO `social_info` VALUES (6, '2020-02-09 14:37:14', NULL, 0, 'fa-search', '#', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for team_info
-- ----------------------------
DROP TABLE IF EXISTS `team_info`;
CREATE TABLE `team_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `img` int(11) NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `service_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_about_us_service_customer_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_about_us_service_customer_info_category_info_category_id`(`category_id`) USING BTREE,
  INDEX `FK_about_us_service_customer_info_image_info_img`(`img`) USING BTREE,
  INDEX `service_id`(`service_id`) USING BTREE,
  CONSTRAINT `team_info_ibfk_1` FOREIGN KEY (`service_id`) REFERENCES `service_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `team_info_ibfk_2` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `team_info_ibfk_3` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `team_info_ibfk_4` FOREIGN KEY (`img`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of team_info
-- ----------------------------
INSERT INTO `team_info` VALUES (1, '2020-02-09 16:50:32', '2020-02-09 18:44:00', 1, 64, 15, NULL, 'Jeffery', 'Jeffery Poole', 1);
INSERT INTO `team_info` VALUES (2, '2020-02-09 16:52:56', '2020-02-09 20:43:29', 1, 65, 15, NULL, 'Jeffery', 'Jeffery Poole', 4);
INSERT INTO `team_info` VALUES (3, '2020-02-09 16:54:39', '2020-02-09 20:43:57', 1, 66, 15, NULL, 'Mike', 'Mike Kennedy', 2);
INSERT INTO `team_info` VALUES (4, '2020-02-09 16:58:48', '2020-02-09 20:44:13', 1, 67, 15, NULL, 'Edwin', 'Edwin Gross', 3);
INSERT INTO `team_info` VALUES (5, '2020-02-09 16:59:50', NULL, 1, 68, 15, NULL, 'Jeffery', 'Jeffery Poole', 1);
INSERT INTO `team_info` VALUES (6, '2020-02-09 17:01:57', '2020-02-09 20:44:40', 1, 69, 15, NULL, 'Mable', 'Mable Schwartz', 4);
INSERT INTO `team_info` VALUES (7, '2020-02-09 17:03:03', '2020-02-09 20:44:54', 1, 70, 15, NULL, 'Adele', 'Adele Washington', 5);
INSERT INTO `team_info` VALUES (8, '2020-02-09 17:05:01', NULL, 1, 71, 15, NULL, 'Jeffery', 'Jeffery Poole', 1);

-- ----------------------------
-- Table structure for team_source_info
-- ----------------------------
DROP TABLE IF EXISTS `team_source_info`;
CREATE TABLE `team_source_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `team_id` int(11) NULL DEFAULT NULL,
  `social_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_about_us_service_customer_source_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_about_us_service_customer_source_info_social_info_social_id`(`social_id`) USING BTREE,
  INDEX `FK_team_info_team_source_info_team_id`(`team_id`) USING BTREE,
  CONSTRAINT `team_source_info_ibfk_1` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `team_source_info_ibfk_2` FOREIGN KEY (`social_id`) REFERENCES `social_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `team_source_info_ibfk_3` FOREIGN KEY (`team_id`) REFERENCES `team_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 37 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of team_source_info
-- ----------------------------
INSERT INTO `team_source_info` VALUES (1, '2020-02-09 18:33:15', NULL, 1, 1, 1, NULL);
INSERT INTO `team_source_info` VALUES (2, '2020-02-09 18:33:15', NULL, 1, 1, 2, NULL);
INSERT INTO `team_source_info` VALUES (3, '2020-02-09 18:33:15', NULL, 1, 1, 3, NULL);
INSERT INTO `team_source_info` VALUES (4, '2020-02-09 18:33:15', NULL, 1, 1, 4, NULL);
INSERT INTO `team_source_info` VALUES (5, '2020-02-09 18:33:15', NULL, 1, 1, 5, NULL);
INSERT INTO `team_source_info` VALUES (6, '2020-02-09 18:41:00', NULL, 1, 2, 1, NULL);
INSERT INTO `team_source_info` VALUES (7, '2020-02-09 18:41:00', NULL, 1, 2, 2, NULL);
INSERT INTO `team_source_info` VALUES (8, '2020-02-09 18:41:00', NULL, 1, 2, 3, NULL);
INSERT INTO `team_source_info` VALUES (9, '2020-02-09 18:41:00', NULL, 0, 2, 4, NULL);
INSERT INTO `team_source_info` VALUES (10, '2020-02-09 18:41:00', NULL, 1, 2, 5, NULL);
INSERT INTO `team_source_info` VALUES (11, '2020-02-09 18:44:21', NULL, 1, 5, 1, NULL);
INSERT INTO `team_source_info` VALUES (12, '2020-02-09 18:44:21', NULL, 1, 5, 2, NULL);
INSERT INTO `team_source_info` VALUES (13, '2020-02-09 18:44:21', NULL, 1, 5, 3, NULL);
INSERT INTO `team_source_info` VALUES (14, '2020-02-09 18:44:21', NULL, 1, 5, 4, NULL);
INSERT INTO `team_source_info` VALUES (15, '2020-02-09 18:44:21', NULL, 1, 5, 5, NULL);
INSERT INTO `team_source_info` VALUES (16, '2020-02-09 18:47:33', NULL, 1, 3, 1, NULL);
INSERT INTO `team_source_info` VALUES (17, '2020-02-09 18:47:33', NULL, 1, 3, 2, NULL);
INSERT INTO `team_source_info` VALUES (18, '2020-02-09 18:47:33', NULL, 1, 3, 3, NULL);
INSERT INTO `team_source_info` VALUES (19, '2020-02-09 18:47:33', NULL, 1, 3, 4, NULL);
INSERT INTO `team_source_info` VALUES (20, '2020-02-09 18:47:33', NULL, 1, 3, 5, NULL);
INSERT INTO `team_source_info` VALUES (21, '2020-02-09 18:48:57', NULL, 1, 4, 1, NULL);
INSERT INTO `team_source_info` VALUES (22, '2020-02-09 18:48:58', NULL, 1, 4, 2, NULL);
INSERT INTO `team_source_info` VALUES (23, '2020-02-09 18:48:58', NULL, 1, 4, 3, NULL);
INSERT INTO `team_source_info` VALUES (24, '2020-02-09 18:48:58', NULL, 1, 4, 4, NULL);
INSERT INTO `team_source_info` VALUES (25, '2020-02-09 18:48:58', NULL, 1, 4, 5, NULL);
INSERT INTO `team_source_info` VALUES (26, '2020-02-09 19:18:16', NULL, 1, 6, 1, NULL);
INSERT INTO `team_source_info` VALUES (27, '2020-02-09 19:18:17', NULL, 1, 6, 2, NULL);
INSERT INTO `team_source_info` VALUES (28, '2020-02-09 19:18:17', NULL, 1, 6, 3, NULL);
INSERT INTO `team_source_info` VALUES (29, '2020-02-09 19:18:17', NULL, 1, 6, 4, NULL);
INSERT INTO `team_source_info` VALUES (30, '2020-02-09 19:18:17', NULL, 1, 6, 5, NULL);
INSERT INTO `team_source_info` VALUES (31, '2020-02-09 19:27:27', NULL, 1, 8, 1, NULL);
INSERT INTO `team_source_info` VALUES (32, '2020-02-09 19:27:27', NULL, 1, 8, 2, NULL);
INSERT INTO `team_source_info` VALUES (33, '2020-02-09 19:27:27', NULL, 1, 8, 3, NULL);
INSERT INTO `team_source_info` VALUES (34, '2020-02-09 19:27:27', NULL, 1, 8, 4, NULL);
INSERT INTO `team_source_info` VALUES (35, '2020-02-09 19:27:27', NULL, 1, 8, 5, NULL);
INSERT INTO `team_source_info` VALUES (36, '2020-02-09 20:38:08', NULL, 1, 7, 2, NULL);

-- ----------------------------
-- Table structure for testimonial_person_info
-- ----------------------------
DROP TABLE IF EXISTS `testimonial_person_info`;
CREATE TABLE `testimonial_person_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `review` int(11) NOT NULL,
  `person_pic` int(50) NULL DEFAULT NULL,
  `testimonial_id` int(11) NULL DEFAULT NULL,
  `enable` tinyint(255) NULL DEFAULT 0,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `IX_testimonial_person_info_testimonial_id`(`testimonial_id`) USING BTREE,
  INDEX `testimonial_person_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_testimonial_person_info_image_info_person_pic`(`person_pic`) USING BTREE,
  CONSTRAINT `FK_testimonial_person_info_category_info_testimonial_id` FOREIGN KEY (`testimonial_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_testimonial_person_info_image_info_person_pic` FOREIGN KEY (`person_pic`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `testimonial_person_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of testimonial_person_info
-- ----------------------------
INSERT INTO `testimonial_person_info` VALUES (1, '2020-01-25 00:03:00', '2020-02-03 23:13:43', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 12, 3, 1, NULL);
INSERT INTO `testimonial_person_info` VALUES (2, '2020-01-13 00:05:15', '2020-02-03 23:17:25', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 13, 3, 1, NULL);
INSERT INTO `testimonial_person_info` VALUES (3, '2020-01-13 00:05:15', '2020-02-03 23:17:40', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 14, 3, 1, NULL);
INSERT INTO `testimonial_person_info` VALUES (4, '2020-01-13 00:05:15', '2020-02-03 23:17:52', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 15, 3, 1, NULL);
INSERT INTO `testimonial_person_info` VALUES (5, '2020-01-13 00:05:15', '2020-02-03 23:18:13', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 16, 3, 1, NULL);
INSERT INTO `testimonial_person_info` VALUES (6, '2020-01-13 00:05:15', '2020-02-03 23:18:21', '-夏洛特·丹尼尔斯', '- Charlotte Daniels', '如果您正在网上查看空白盒带，您可能会对价差感到困惑。', 'If you are looking at blank cassettes on the web, you may be very confused at the difference in price.', 0, 17, 3, 1, NULL);

-- ----------------------------
-- Table structure for theme_info
-- ----------------------------
DROP TABLE IF EXISTS `theme_info`;
CREATE TABLE `theme_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `href` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `enable` tinyint(255) NULL DEFAULT 0,
  `admin_id` int(11) NULL DEFAULT NULL,
  `category_id` int(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `theme_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_theme_info_category_info_category_id`(`category_id`) USING BTREE,
  CONSTRAINT `FK_theme_info_category_info_category_id` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `theme_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of theme_info
-- ----------------------------
INSERT INTO `theme_info` VALUES (1, '2020-01-13 00:05:14', NULL, '关于我们', 'About us', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (2, '2020-01-13 00:05:14', NULL, '锻炼', 'Exercitation', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (3, '2020-01-13 00:05:14', NULL, '维尼亚姆', 'Veniam', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (4, '2020-01-13 00:05:14', NULL, '临时的', 'Tempor', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (5, '2020-01-13 00:05:14', NULL, '尤斯莫德', 'Eiusmod', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (6, '2020-01-13 00:05:14', NULL, '提倡精英', 'Adipisicing Elit', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (7, '2020-01-13 00:05:14', NULL, '文章写作', 'Article Writing', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (8, '2020-01-13 00:05:14', NULL, '插件开发', 'Plugin Development', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (9, '2020-01-13 00:05:14', NULL, '电子邮件营销', 'Email Marketing', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (10, '2020-01-13 00:05:14', NULL, '发展历程', 'Development', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (11, '2020-01-13 00:05:14', NULL, '主题', 'Theme', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (12, '2020-01-13 00:05:14', NULL, 'SEO营销', 'SEO Marketing', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (13, '2020-01-13 00:05:14', NULL, '乌拉姆科', 'Ullamco', '#', 1, NULL, 7);
INSERT INTO `theme_info` VALUES (14, '2020-01-13 00:05:14', NULL, 'Web开发', 'Web Development', '#', 1, NULL, 6);
INSERT INTO `theme_info` VALUES (15, '2020-01-13 00:05:14', NULL, '票务系统', 'Ticket system', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (16, '2020-01-13 00:05:14', NULL, '退款政策', 'Refund policy', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (17, '2020-01-13 00:05:14', NULL, '文献资料', 'Documentation', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (18, '2020-01-13 00:05:14', NULL, '论坛', 'Forum', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (19, '2020-01-13 00:05:14', NULL, '博客', 'Blog', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (20, '2020-01-13 00:05:14', NULL, '联系我们', 'Contact us', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (21, '2020-01-13 00:05:14', NULL, '隐私政策', 'Privacy policy', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (22, '2020-01-13 00:05:14', NULL, '使用条款', 'Terms of use', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (23, '2020-01-13 00:05:14', NULL, '版权', 'Copyright', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (24, '2020-01-13 00:05:14', NULL, '认识团队', 'Meet the team', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (25, '2020-01-13 00:05:14', NULL, '我们正在招聘', 'We are hiring', '#', 1, NULL, 4);
INSERT INTO `theme_info` VALUES (26, '2020-01-13 00:05:14', NULL, '计费系统', 'Billing system', '#', 1, NULL, 5);
INSERT INTO `theme_info` VALUES (27, '2020-01-13 00:05:14', NULL, '劳比斯', 'Laboris', '#', 1, NULL, 7);

-- ----------------------------
-- Table structure for user_info
-- ----------------------------
DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `account` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `phone` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `email` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `sex` tinyint(1) NOT NULL,
  `remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `city` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `file` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for work_category_info
-- ----------------------------
DROP TABLE IF EXISTS `work_category_info`;
CREATE TABLE `work_category_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `name` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `english_name` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `filter` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `work_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  `parent_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_recent_work_category_info_admin_info_admin_id`(`admin_id`) USING BTREE,
  INDEX `FK_recent_work_category_info_recent_work_info_recent_work_id`(`work_id`) USING BTREE,
  INDEX `parent_id`(`parent_id`) USING BTREE,
  CONSTRAINT `FK_recent_work_category_info_admin_info_admin_id` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_recent_work_category_info_recent_work_info_recent_work_id` FOREIGN KEY (`work_id`) REFERENCES `work_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `work_category_info_ibfk_1` FOREIGN KEY (`parent_id`) REFERENCES `work_category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of work_category_info
-- ----------------------------
INSERT INTO `work_category_info` VALUES (1, '2020-02-10 19:59:21', '2020-02-12 16:09:58', 1, '所有作品', '所有作品', '*', NULL, NULL, 1);
INSERT INTO `work_category_info` VALUES (2, '2020-02-10 20:00:01', '2020-02-12 16:03:30', 1, '有创意', 'All Works', '.bootstrap', NULL, NULL, 1);
INSERT INTO `work_category_info` VALUES (5, '2020-02-10 20:25:01', '2020-02-12 16:03:48', 1, '摄影', 'Photography', '.html', NULL, NULL, 1);
INSERT INTO `work_category_info` VALUES (6, '2020-02-10 20:25:21', '2020-02-12 16:03:54', 1, 'Web开发', 'Web Development', '.wordpress', NULL, NULL, 1);
INSERT INTO `work_category_info` VALUES (7, '2020-02-12 15:39:55', '2020-02-12 17:09:40', 1, NULL, NULL, NULL, 1, NULL, 2);
INSERT INTO `work_category_info` VALUES (8, '2020-02-12 15:40:02', '2020-02-12 17:09:45', 1, NULL, NULL, NULL, 2, NULL, 6);
INSERT INTO `work_category_info` VALUES (9, '2020-02-12 15:40:07', NULL, 1, NULL, NULL, NULL, 3, NULL, 2);
INSERT INTO `work_category_info` VALUES (10, '2020-02-12 16:30:20', '2020-02-12 17:10:03', 1, NULL, NULL, NULL, 4, NULL, 6);
INSERT INTO `work_category_info` VALUES (11, '2020-02-12 16:30:25', '2020-02-12 17:10:10', 1, NULL, NULL, NULL, 6, NULL, 2);
INSERT INTO `work_category_info` VALUES (12, '2020-02-12 16:30:34', '2020-02-12 17:10:44', 1, NULL, NULL, NULL, 5, NULL, 5);
INSERT INTO `work_category_info` VALUES (13, '2020-02-12 17:09:00', '2020-02-12 17:10:22', 1, NULL, NULL, NULL, 7, NULL, 6);
INSERT INTO `work_category_info` VALUES (14, '2020-02-12 18:05:52', NULL, 1, NULL, NULL, NULL, 5, NULL, 5);
INSERT INTO `work_category_info` VALUES (15, '2020-02-12 19:21:10', NULL, 1, NULL, NULL, NULL, 1, NULL, 1);
INSERT INTO `work_category_info` VALUES (16, '2020-02-12 19:27:55', NULL, 1, NULL, NULL, NULL, 1, NULL, 1);
INSERT INTO `work_category_info` VALUES (17, '2020-02-12 19:28:00', NULL, 1, NULL, NULL, NULL, 2, NULL, 1);
INSERT INTO `work_category_info` VALUES (18, '2020-02-12 19:28:07', NULL, 1, NULL, NULL, NULL, 3, NULL, 1);
INSERT INTO `work_category_info` VALUES (19, '2020-02-12 19:28:10', NULL, 1, NULL, NULL, NULL, 4, NULL, 1);
INSERT INTO `work_category_info` VALUES (20, '2020-02-12 19:28:14', NULL, 1, NULL, NULL, NULL, 5, NULL, 1);
INSERT INTO `work_category_info` VALUES (21, '2020-02-12 19:28:33', NULL, 1, NULL, NULL, NULL, 2, NULL, 2);
INSERT INTO `work_category_info` VALUES (22, '2020-02-12 19:28:39', NULL, 1, NULL, NULL, NULL, 4, NULL, 2);
INSERT INTO `work_category_info` VALUES (23, '2020-02-12 19:28:43', NULL, 1, NULL, NULL, NULL, 6, NULL, 2);
INSERT INTO `work_category_info` VALUES (24, '2020-02-12 19:28:49', NULL, 1, NULL, NULL, NULL, 6, NULL, 5);
INSERT INTO `work_category_info` VALUES (25, '2020-02-12 19:28:57', NULL, 1, NULL, NULL, NULL, 7, NULL, 5);
INSERT INTO `work_category_info` VALUES (26, '2020-02-12 19:29:02', NULL, 1, NULL, NULL, NULL, 4, NULL, 6);
INSERT INTO `work_category_info` VALUES (27, '2020-02-12 19:29:06', NULL, 1, NULL, NULL, NULL, 4, NULL, 6);

-- ----------------------------
-- Table structure for work_info
-- ----------------------------
DROP TABLE IF EXISTS `work_info`;
CREATE TABLE `work_info`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `create_date` datetime NOT NULL,
  `modify_date` datetime NULL DEFAULT NULL,
  `enable` tinyint(1) NULL DEFAULT NULL,
  `img_id` int(11) NULL DEFAULT NULL,
  `category_id` int(11) NULL DEFAULT NULL,
  `admin_id` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `FK_recent_work_info_category_info_category_id`(`category_id`) USING BTREE,
  INDEX `FK_recent_work_info_image_info_img_id`(`img_id`) USING BTREE,
  INDEX `admin_id`(`admin_id`) USING BTREE,
  CONSTRAINT `FK_recent_work_info_category_info_category_id` FOREIGN KEY (`category_id`) REFERENCES `category_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_recent_work_info_image_info_img_id` FOREIGN KEY (`img_id`) REFERENCES `image_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `work_info_ibfk_1` FOREIGN KEY (`admin_id`) REFERENCES `admin_info` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of work_info
-- ----------------------------
INSERT INTO `work_info` VALUES (1, '2020-02-05 11:31:23', NULL, 1, 31, 13, NULL);
INSERT INTO `work_info` VALUES (2, '2020-02-05 11:32:16', NULL, 1, 33, 13, NULL);
INSERT INTO `work_info` VALUES (3, '2020-02-05 11:32:17', NULL, 1, 34, 13, NULL);
INSERT INTO `work_info` VALUES (4, '2020-02-05 11:32:17', NULL, 1, 35, 13, NULL);
INSERT INTO `work_info` VALUES (5, '2020-02-05 11:32:17', NULL, 1, 36, 13, NULL);
INSERT INTO `work_info` VALUES (6, '2020-02-05 11:32:17', NULL, 1, 37, 13, NULL);
INSERT INTO `work_info` VALUES (7, '2020-02-05 11:32:17', NULL, 1, 38, 13, NULL);

-- ----------------------------
-- Procedure structure for POMELO_AFTER_ADD_PRIMARY_KEY
-- ----------------------------
DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
delimiter ;;
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for POMELO_BEFORE_DROP_PRIMARY_KEY
-- ----------------------------
DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
delimiter ;;
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
