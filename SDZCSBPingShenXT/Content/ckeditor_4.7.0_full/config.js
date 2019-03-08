/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';


    //定义上传的路径 filebrowserImageUploadUrl 同时也要注意浏览器缓存的问题 可以换个浏览器，清空浏览器
    config.filebrowserImageUploadUrl = '/UnitHome/Notice/UpEeditorFile';
};
