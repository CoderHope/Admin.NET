/* tslint:disable */
/* eslint-disable */
/**
 * Admin.NET
 * 让 .NET 开发更简单、更通用、更流行。前后端分离架构(.NET6/Vue3)，开箱即用紧随前沿技术。<br/><a href='https://gitee.com/zuohuaijun/Admin.NET/'>https://gitee.com/zuohuaijun/Admin.NET</a>
 *
 * OpenAPI spec version: 1.0.0
 * Contact: 515096995@qq.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { DataItem } from './data-item';
/**
 * 发送订阅消息
 * @export
 * @interface SendSubscribeMessageInput
 */
export interface SendSubscribeMessageInput {
    /**
     * 订阅模板Id
     * @type {string}
     * @memberof SendSubscribeMessageInput
     */
    templateId: string;
    /**
     * 接收者的OpenId
     * @type {string}
     * @memberof SendSubscribeMessageInput
     */
    toUserOpenId: string;
    /**
     * 模板内容，格式形如 { \"key1\": { \"value\": any }, \"key2\": { \"value\": any } }
     * @type {{ [key: string]: DataItem; }}
     * @memberof SendSubscribeMessageInput
     */
    data: { [key: string]: DataItem; };
    /**
     * 跳转小程序类型
     * @type {string}
     * @memberof SendSubscribeMessageInput
     */
    miniprogramState?: string | null;
    /**
     * 语言类型
     * @type {string}
     * @memberof SendSubscribeMessageInput
     */
    language?: string | null;
    /**
     * 点击模板卡片后的跳转页面（仅限本小程序内的页面），支持带参数（示例pages/app/index?foo=bar）
     * @type {string}
     * @memberof SendSubscribeMessageInput
     */
    miniProgramPagePath?: string | null;
}
